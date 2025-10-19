using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this using directive

namespace MangaBook_Models
{
    public class MangaDbContext : IdentityDbContext<MangaUser>
    {
        public DbSet<MangaBook> MangaBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = "Server=localhost;Database=AgendaDb;User Id=sa;Password=Your_password123;MultipleActiveResultSets=true";
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=MangaDbContext;Trusted_Connection=true;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }


        //dit is voor de seeding van de database met dummy data
        //het checkt of er data is of niet, zo nie dan vult hij de tabellen met dummy data
        public static void Seeder(MangaDbContext context)
        {
            MangaUser.Seeder().Wait();

            if (!context.Authors.Any())
            {
                context.Authors.AddRange(Author.SeedingData());
                context.SaveChanges();
            }

            if (!context.Genres.Any())
            {
                context.Genres.AddRange(Genre.SeedingData());
                context.SaveChanges();
            }


            if (!context.MangaBooks.Any())
            {
                context.MangaBooks.AddRange(MangaBook.SeedingData());
                context.SaveChanges();
            }
        }
    }
}
