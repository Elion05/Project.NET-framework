using MangaBook_Models;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        static public ServiceProvider ServiceProvider { get; private set; }
        static public MangaUsers User { get; set; }
        static public MainWindow MainWindow { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddDbContext<MangaDbContext>();

            services.AddIdentityCore<MangaUsers>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MangaDbContext>();


            services.AddLogging();
            ServiceProvider = services.BuildServiceProvider();


            MangaDbContext mangaContext = new MangaDbContext();
            MangaDbContext.Seeder(mangaContext);

            App.User = MangaUsers.bob;
            //MainWindow = new MainWindow(App.ServiceProvider.GetRequiredService<MangaDbContext>());
            //MainWindow.Show();
        }


       

    }

}
