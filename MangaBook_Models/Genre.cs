using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design;
using System.Text;
using System.Threading.Tasks;

namespace MangaBook_Models
{
    public class Genre
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Genre naam")]
        public string Name { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Beschrijving")]
        public string genreBeschrijving { get; set; } = string.Empty;



        //dummy data
        public static List<Genre> SeedingData()
        {
            var list = new List<Genre>
            {
                new Genre { Name = "Shonen", genreBeschrijving = "Shonen richt zich vooral op jonge mannelijke lezers en bevat vaak actie, avontuur, vriendschap en doorzettingsvermogen met energieke hoofdpersonages." },
                new Genre { Name = "Shojo" , genreBeschrijving = "Shojo manga richt zich op jonge vrouwelijke lezers en draait vaak om romantiek, emoties, relaties en persoonlijke groei in een elegante stijl."},
                new Genre { Name = "Seinen" , genreBeschrijving = "Seinen is bedoeld voor volwassen mannen en bevat vaak complexe verhalen, realistische thema’s, geweld, drama en diepgaande karakterontwikkeling." },
                new Genre { Name = "Josei", genreBeschrijving = "Josei manga richt zich op volwassen vrouwen en verkent realistische relaties, werk, liefde en volwassen emoties met een serieuze toon." },
                new Genre { Name = "Kodomo", genreBeschrijving = "Kodomo is gericht op jonge kinderen en bevat eenvoudige, vrolijke verhalen met duidelijke moraal, humor en kleurrijke karakters."},
                new Genre { Name = "Mecha" , genreBeschrijving = "Mecha draait om gigantische robots of machines die vaak door mensen bestuurd worden en combineert actie, technologie en sciencefiction."},
                new Genre { Name = "Isekai",  genreBeschrijving = "Isekai vertelt het verhaal van personages die naar een andere wereld worden getransporteerd, meestal vol magie, avontuur en herontdekking." },
                new Genre {Name = "Sport", genreBeschrijving = "Sport manga draait om competitie, teamwork, training en persoonlijke groei, met inspirerende verhalen rond verschillende sporten en toernooien."},
                new Genre { Name = "Komedie", genreBeschrijving = "Komedie manga richt zich op humor en grappige situaties, vaak met excentrieke personages en absurde plots." },
                new Genre { Name = "Sci-Fi", genreBeschrijving = "Sciencefiction manga verkent futuristische concepten, technologie en de impact daarvan op de samenleving." },
                new Genre { Name = "Historisch", genreBeschrijving = "Historische manga speelt zich af in het verleden en kan zowel feitelijke als fictieve gebeurtenissen en personages bevatten." },
                new Genre { Name = "Magical Girl", genreBeschrijving = "Magical Girl is een subgenre van shojo waarin meisjes magische krachten krijgen om het kwaad te bestrijden." },
                new Genre { Name = "Romantiek", genreBeschrijving = "Romantische manga richt zich op liefdesverhalen en de ontwikkeling van relaties tussen personages." },
                new Genre { Name = "Drama", genreBeschrijving = "Drama manga richt zich op realistische verhalen, emotionele conflicten en karakterontwikkeling." }
            };
            return list;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
