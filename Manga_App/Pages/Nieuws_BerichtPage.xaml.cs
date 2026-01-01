using Manga_App.ViewModels;

namespace Manga_App.Pages;

public partial class Nieuws_BerichtPage : ContentPage
{
	public Nieuws_BerichtPage(Nieuws_BerichtViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }


	protected override async void OnAppearing()
	{
		base.OnAppearing();
		//Een call om de LoadNieuwsBerichtenCommand op te roepen
		if (BindingContext is Nieuws_BerichtViewModel vm)
		{
			await vm.LoadNieuwsBerichtenCommand.ExecuteAsync(null);
		}
    }
}