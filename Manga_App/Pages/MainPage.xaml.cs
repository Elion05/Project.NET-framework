using Manga_App.Pages;
using Manga_App.ViewModels;
using MangaBook_Models;

namespace Manga_App;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void HomeButton_Clicked(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var vm = new HomeViewModel(context);
        await Navigation.PushAsync(new HomePage(vm));
    }

    private async void OnLoginPageRequested(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var loginViewModel = new LoginViewModel(context);
        var loginPage = new LoginPage(loginViewModel, context);

        // Abonneer op het LoginSucceeded event
        loginViewModel.LoginSucceeded += async (s, args) =>
        {
            
            await Navigation.PopAsync();

            
            var homeVm = new HomeViewModel(context);
            await Navigation.PushAsync(new HomePage(homeVm));
        };

        await Navigation.PushAsync(loginPage);
    }
}
