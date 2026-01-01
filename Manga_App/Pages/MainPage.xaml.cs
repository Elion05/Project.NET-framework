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

    private async void MijnBoekenButton_Clicked(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var viewModel = new MangaBookViewModel(new MangaBook(), context);
        await Navigation.PushAsync(new MangaBookPage(viewModel));
    }

    private async void AuthorsButton_Clicked(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var viewModel = new AuthorViewModel(new Author(), context);
        await Navigation.PushAsync(new AuthorPage(viewModel));
    }

    private async void GenreButton_Clicked(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var viewModel = new GenreViewModel(new Genre(), context);
        await Navigation.PushAsync(new GenrePage(viewModel));
    }

    private async void NieuwsBerichtButton_Clicked(object sender, EventArgs e)
    {
        var context = new LocalDbContext();
        var viewModel = new Nieuws_BerichtViewModel(new Nieuws_Bericht(), context);
        await Navigation.PushAsync(new Nieuws_BerichtPage(viewModel));
    }

}
