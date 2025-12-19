using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace MangaBook_Models.Data
{
    public class MangaDbContext : IdentityDbContext<MangaUser>
    {
        //constructor zodat je opties kan doorgeven bij het aanmaken van de context in de Manga_Web project


        //zodat je tabellen hebt in de database
        public DbSet<MangaBook> MangaBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Nieuws_Bericht> Nieuws_Berichten { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LogError> LogErrors { get; set; }

        //Fallback optie voor het geval er geen opties worden doorgegeven
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //default connection string voor lokaal gebruik
            string connectionString = "Server =(localdb)\\mssqllocaldb;Database=MangaDbContext;Trusted_Connection=true;MultipleActiveResultSets=true";


            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    var config = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddUserSecrets<MangaDbContext>(optional: true)
                        .AddEnvironmentVariables()
                        .Build();

                    string con = config.GetConnectionString("MangaDbContextConnection");
                }
                catch(Exception ex)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        //dit is voor de seeding van de database met dummy data
        public static async Task Seeder(MangaDbContext context)
        {
            Language.Seeder();

            MangaUser.Seeder();

            //3) Seeding van dummyData, direct bijgevuld in de database
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

            if (!context.Nieuws_Berichten.Any())
            {
                context.Nieuws_Berichten.AddRange(new List<Nieuws_Bericht>
                {
                    new Nieuws_Bericht
                    {
                        Titel = "Welkom bij MangaBook!",
                        Inhoud = "Random text hier blahblahblahlbahlvah",
                        GebruikerId = context.Users.FirstOrDefault(u => u.UserName == "admin")?.Id??"",
                        Datum = DateTime.Now,
                    }
                });
                context.SaveChanges();
            }
        }
    }
}