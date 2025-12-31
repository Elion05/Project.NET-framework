using System;
using Manga_App.ViewModels;
using Microsoft.Maui.Controls;

namespace Manga_App.Pages;

public partial class MangaBookPage : ContentPage
{
	public MangaBookPage(MangaBookViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


	protected override async void OnAppearing()
	{
		base.OnAppearing();

		//Een call om de LoadMangaBooksCommand op te roepen
		if (BindingContext is MangaBookViewModel vm)
		{
			await vm.LoadMangaBooksCommand.ExecuteAsync(null);
		}
	}


	private void MijnBoekenButton_Clicked(object sender, EventArgs e)
	{
		
	}
}