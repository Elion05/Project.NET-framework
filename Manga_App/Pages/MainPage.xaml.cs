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
}
