using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MangaBook_Models
{
    public class MangaUser : IdentityUser
    {

        //5) Identity Framework: Je voorziet een eigen user-class met minstens één extra eigenschap voor je gebruikers
        
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        [ForeignKey("Language")]
        public string LanguageCode { get; set; } = "nl";

        public Language? Language { get; set; }

        public bool IsDeleted { get; set; } = false;


        public static MangaUser Dummy = new MangaUser
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "-",
            LastName = "-",
            UserName = "dummy",
            NormalizedUserName = "DUMMY",
            Email = "dummy@manga.be",
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.MaxValue,
            LanguageCode = "-",
        };

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }




        //7) Rollen, 
        public static async Task Seeder()
        {
            MangaDbContext context = new MangaDbContext();

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<IdentityRole>
                {
                    new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" },
                });
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Add(Dummy);
                context.SaveChanges();

                var admin = new MangaUser
                {
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@manga.be",
                    EmailConfirmed = true,
                };

                var normaleUser = new MangaUser
                {
                    UserName = "user",
                    FirstName = "Normal",
                    LastName = "User",
                    Email = "user@manga.be",
                    EmailConfirmed = true,
                };

                var userManager = new UserManager<MangaUser>(
                    new UserStore<MangaUser>(context),
                    null, new PasswordHasher<MangaUser>(),
                    null, null, null, null, null, null
                );

                try
                {
                    await userManager.CreateAsync(admin, "Admin123!");
                    await userManager.CreateAsync(normaleUser, "User123!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating users: {ex.Message}");
                }


                while (context.Users.Count() < 2)
                {
                    await Task.Delay(100);
                }

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(normaleUser, "User");

            }

            


           

            Dummy = context.Users.First(u => u.UserName == "dummy");
        }
    }
}
