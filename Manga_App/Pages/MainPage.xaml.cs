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
}
