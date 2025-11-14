using CommunityToolkit.Mvvm.ComponentModel;
using MangaBook_Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Manga_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        public MainViewModel()
        {
            mangaBooks = new ObservableCollection<MangaBook>();
        }

        [ObservableProperty]
        ObservableCollection<MangaBook> mangaBooks = new ObservableCollection<MangaBook>();


        [ObservableProperty]
        string mangaBookName;

        [ObservableProperty]
        string auteurBookName;


      
       

    }
}
