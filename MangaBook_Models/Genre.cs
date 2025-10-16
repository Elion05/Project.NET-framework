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


        //dummy data
        public static List<Genre> SeedingData()
        {
            var list = new List<Genre>
            {
                new Genre { Name = "Shonen" },
                new Genre { Name = "Shojo" },
                new Genre { Name = "Seinen" },
                new Genre { Name = "Josei" },
                new Genre { Name = "Kodomo" },
                new Genre { Name = "Mecha" },
                new Genre { Name = "Isekai" },
                new Genre {Name = "Sport"},
            };
            return list;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
