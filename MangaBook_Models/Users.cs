using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace MangaBook_Models
{
    public class MangaUsers : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;



        public static MangaUsers bob = new MangaUsers
        {
            Id = "-",
            FirstName = "-",
            LastName = "-",
            UserName = "Bob",
            NormalizedUserName = "BOB",
            Email = "Bob@Manga.be",
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.MaxValue
        };

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static async Task Seeder()
        {
            MangaDbContext context = new MangaDbContext();

            if (!context.Set<IdentityRole>().Any())
            {
                context.AddRange(new List<IdentityRole>
                {
                    new IdentityRole{ Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole{ Id ="SystemAdmin", Name = "SystemAdmin", NormalizedName = "SYSTEMADMIN" },
                    new IdentityRole{ Id = "UserAdmin" , Name = "UserAdmin", NormalizedName = "USERADMIN" },
                    new IdentityRole{ Id = "User", Name = "User", NormalizedName = "USER" },
                });
                context.SaveChanges();
            }

            // Fix for CS1061 and CS0642
            // Replace: if(!context.Users.Any());
            // With:    if (!context.Set<MangaUsers>().Any())
            if (!context.Set<MangaUsers>().Any())
            {
                context.Add(bob);
                context.SaveChanges();
                MangaUsers user = new MangaUsers { UserName = "user", FirstName = "user", LastName = "user1", Email = "user@Manga.be", EmailConfirmed = true };
                MangaUsers admin = new MangaUsers { UserName = "admin", FirstName = "Admin", LastName = "admin1", Email = "admin@Manga.be", EmailConfirmed = true};
                MangaUsers systemAdmin = new MangaUsers { UserName = "systemadmin", FirstName = "System", LastName = "Admin", Email = "systemadmin@manga.be", EmailConfirmed = true };
                MangaUsers userAdmin = new MangaUsers { UserName = "useradmin", FirstName = "User", LastName = "Admin", Email = "useradmin@manga.be", EmailConfirmed = true };

                UserManager<MangaUsers> userManager = new UserManager<MangaUsers>(
                    new UserStore<MangaUsers>(context),
                    null, new PasswordHasher<MangaUsers>(),
                    null, null, null, null, null, null
                    );

                await userManager.CreateAsync(user, "123456aze!");
                await userManager.CreateAsync(admin, "123456aze!");
                await userManager.CreateAsync(systemAdmin, "123456aze!");
                await userManager.CreateAsync(userAdmin, "123456aze!");

                while(context.Set<MangaUsers>().Count() < 5)
                {
                    await Task.Delay(100);
                }

                await userManager.AddToRoleAsync(user, "User");
                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(systemAdmin, "SystemAdmin");
                await userManager.AddToRoleAsync(userAdmin, "UserAdmin");
            }

            bob = context.Set<MangaUsers>().First(u => u.UserName == "Bob");

        }
    }
}
