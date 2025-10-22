using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MangaBook_Models
{
    public class MangaUser : IdentityUser
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;


        public static MangaUser Dummy = new MangaUser
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "-",
            LastName = "-",
            UserName = "dummy",
            NormalizedUserName = "DUMMY",
            Email = "dummy@manga.be",
            LockoutEnabled = true,
            LockoutEnd = DateTimeOffset.MaxValue
        };

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static async Task Seeder(MangaDbContext context)
        {
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

                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.CreateAsync(normaleUser, "User123!");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(normaleUser, "User");

            }

            Dummy = context.Users.First(u => u.UserName == "dummy");
        }
    }
}
