using Manga_App.Pages;
using MangaBook_Models;
using Microsoft.Extensions.Logging;

namespace Manga_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

           builder.Services.AddDbContext<LocalDbContext>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<ViewModels.LoginViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ViewModels.MainViewModel>();

            builder.Services.AddTransient<Pages.MangaBookPage>();
            builder.Services.AddTransient<ViewModels.MangaBookViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
