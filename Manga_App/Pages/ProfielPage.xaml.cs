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

	private async void HomeButton_Clicked(object sender, EventArgs e)
	{
		var context = new LocalDbContext();
		var vm = new HomeViewModel(context);
		await Navigation.PushAsync(new HomePage(vm));
	}
}