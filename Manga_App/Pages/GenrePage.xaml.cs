using Manga_App.ViewModels;

namespace Manga_App.Pages;

public partial class GenrePage : ContentPage
{
	public GenrePage(GenreViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//Roep de command aan om data te laden
		if (BindingContext is GenreViewModel vm)
		{
			await vm.LoadGenresCommand.ExecuteAsync(null);
		}
    }
}