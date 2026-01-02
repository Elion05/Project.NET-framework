using Manga_App.Pages;
using MangaBook_Models;
using Manga_App.ViewModels;


namespace Manga_App.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
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

    private async void ProfielButton_Clicked(object sender, EventArgs e)
    {
        if(General.User != null && !string.IsNullOrEmpty(General.UserId) && General.UserId.Length > 10)
        {
            await Navigation.PushAsync(new ProfielPage(General.User));
        }
        else
        {
            await DisplayAlert("Niet ingelogd", "Je moet ingelogd zijn om je profiel te bekijken.", "OK");
        }
    }

   
}