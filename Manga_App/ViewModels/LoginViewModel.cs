using MangaBook_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Manga_App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        LocalDbContext _context;

        public LoginViewModel(LocalDbContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        LoginModel loginModel;

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        bool rememberMe;

        [ObservableProperty]
        string message;

        [ObservableProperty]
        bool isMessageVisible = false;

        [RelayCommand]
        async Task Login()
        {
            LoginModel loginModel = new LoginModel()
            {
                Username = UserName,
                Password = Password,
                RememberMe = RememberMe
            };

            Synchronizer synchronizer = new Synchronizer(_context);
            bool result = await synchronizer.Login(loginModel);
            if (result)
            {
                if (Application.Current?.MainPage?.Navigation != null)
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            else
            {
                isMessageVisible = true;
                Message = "Slechte login, probeer opnieuw of registreer je.";
            }

        }
    }
}
