using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class App : System.Windows.Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        public static MangaUser? User { get; set; }
        public new static MainWindow? MainWindow { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup Dependency Injection
            var services = new ServiceCollection();

            // Voeg DbContext en Identity toe
            services.AddDbContext<MangaDbContext>();

            services.AddIdentityCore<MangaUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MangaDbContext>();

            services.AddLogging();
            ServiceProvider = services.BuildServiceProvider();

            // Maak database aan en seed data
            var context = new MangaDbContext();
            context.Database.Migrate(); // Ensure database and tables are created
            MangaDbContext.Seeder(context);

            // Stel standaardgebruiker in
            App.User = MangaUser.Dummy;

            
        }
    }
}