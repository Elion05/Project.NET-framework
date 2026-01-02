using MangaBook_Models;
using Manga_App.ViewModels;

namespace Manga_App.Pages;

public partial class ProfielPage : ContentPage
{
	public ProfielPage(MangaUser user)
	{
		InitializeComponent();
		BindingContext = new ProfielViewModel(user);
	}
}