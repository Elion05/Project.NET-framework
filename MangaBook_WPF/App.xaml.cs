using MangaBook_Models;
using MangaBook_Models.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class App : System.Windows.Application
    {
        
        public static ServiceProvider? ServiceProvider { get; private set; } //deze zorgt ervoor dat de services beschikbaar zijn in de hele applicatie

        public static MangaUser? User { get; set; }//dit zorgt ervoor dat de ingelogde gebruiker beschikbaar is in de hele project
        public new static MainWindow? MainWindow { get; set; }//dit zorgt ervoor dat de mainwindow beschikbaar is in de hele project

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //dit voegt services toe aan de servicecollection
            var services = new ServiceCollection();

            //dit voegt de databasecontext toe aan de services
            services.AddDbContext<MangaDbContext>();

            //dit voegt identity toe aan de services
            services.AddIdentityCore<MangaUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MangaDbContext>();

            //dit voegt logging toe aan de services
            services.AddLogging();
            ServiceProvider = services.BuildServiceProvider();

            //Maak database aan en seed data
            var context = new MangaDbContext();
            context.Database.Migrate(); // Ensure database and tables are created
            MangaDbContext.Seeder(context);

            // Stel standaardgebruiker in
            App.User = MangaUser.Dummy;
        }
    }
}