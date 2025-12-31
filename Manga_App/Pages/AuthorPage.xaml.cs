using Manga_App.ViewModels;
using MangaBook_Models;

namespace Manga_App.Pages;

public partial class AuthorPage : ContentPage
{
    public AuthorPage(AuthorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //Roep de command aan om data te laden
        if (BindingContext is AuthorViewModel vm)
        {
            await vm.LoadAuthorsCommand.ExecuteAsync(null);
        }
    }
}