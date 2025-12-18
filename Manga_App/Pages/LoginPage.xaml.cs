using SQLitePCL;
using Manga_App.ViewModels;
using MangaBook_Models;

namespace Manga_App.Pages;

public partial class LoginPage : ContentPage
{
	readonly LocalDbContext _context;

	public LoginPage(LoginViewModel viewModel, LocalDbContext context)
	{
		_context = context;
		InitializeComponent();
		BindingContext = viewModel;

	}
}