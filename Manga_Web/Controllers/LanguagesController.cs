using MangaBook_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Manga_Web.Controllers
{

    
    public class LanguagesController : Controller
    {
        private readonly MangaDbContext _context;
        public LanguagesController(MangaDbContext context)
        {
            _context = context;
        }
        

        //deze class dient voor de cookie op te slagen
        public IActionResult ChangeLanguage(string code, string returnUrl)
        {
            
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(code)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(2) }
                );

            return LocalRedirect(returnUrl);       
        }
    }
}
