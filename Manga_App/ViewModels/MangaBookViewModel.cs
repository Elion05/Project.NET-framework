using CommunityToolkit.Mvvm.ComponentModel;
using MangaBook_Models;


namespace Manga_App.ViewModels
{
    public partial class MangaBookViewModel : ObservableObject
    {
        [ObservableProperty]
        MangaBook book;

        public MangaBookViewModel(MangaBook book)
        {
          Book = book;
        }

    }
}

