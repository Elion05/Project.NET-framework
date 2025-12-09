using Manga_App.ViewModels;

namespace Manga_App.Pages;

public partial class MangaBookPage : ContentPage
{
	public MangaBookPage(MangaBookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}