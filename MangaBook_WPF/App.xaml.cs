using MangaBook_Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            MangaDbContext mangaContext = new MangaDbContext();
            MangaDbContext.Seeder(mangaContext);
        }
    }

}
