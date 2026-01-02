using CommunityToolkit.Mvvm.ComponentModel;
using MangaBook_Models;


namespace Manga_App.ViewModels
{
    public partial class ProfielViewModel : ObservableObject
    {

        [ObservableProperty] string firstName;
        [ObservableProperty] string lastName;
        [ObservableProperty] string username;
        [ObservableProperty] string email;

        public ProfielViewModel(MangaUser user)
        {
            firstName = user.FirstName;
            lastName = user.LastName;
            username = user.UserName;
            email = user.Email;
        }
    }
}
