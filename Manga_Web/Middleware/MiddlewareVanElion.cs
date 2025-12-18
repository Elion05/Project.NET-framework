using MangaBook_Models;
using MangaBook_Models.Migrations;
using MangaBook_Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Manga_Web.Middleware
{
    public class MijnGebruiker
    {
        //dit is nodig om request af te handelen
        readonly RequestDelegate _next;

        //dit is een voorbeeld van een in memory opslag van gebruikers
        Dictionary<string, string> Gebruikers;

        MangaDbContext _context;


        public MijnGebruiker(RequestDelegate next)
        {

            //dit de middleware constructor
            Gebruikers = new Dictionary<string, string>();
            Gebruikers["?"] = MangaUser.Dummy.Id;
            _context = new MangaDbContext();


            //als MijnGebruiker is afgehandeld, ga dan naar de volgende middleware in de pipeline
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Dit is de code die wordt uitgevoerd voor elke HTTP-aanvraag
            string gebruikerNaam = httpContext.User.Identity?.Name;
            string userId = "";


            if (gebruikerNaam != null && !Gebruikers.ContainsKey(gebruikerNaam))
            {
                //simuleer het ophalen van de gebruiker uit de database
                if (Gebruikers.ContainsKey(gebruikerNaam))

                    userId = Gebruikers[gebruikerNaam];

                if (userId == "")
                {
                    //voeg gebruiker toe aan de HttpContext-items voor later
                    userId = (await _context.Users.FirstAsync(u => u.UserName == gebruikerNaam)).Id;
                    Gebruikers[gebruikerNaam] = userId;
                }
            }
            else userId = MangaUser.Dummy.Id;
            httpContext.Items["UserId"] = userId;


            await _next(httpContext);
        }
    }
}
