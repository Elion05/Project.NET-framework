using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;
using System.Collections.ObjectModel;


namespace Manga_App.ViewModels
{
    public partial class GenreViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<Genre> genres = new();


        Genre genre;
        readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        //constructor om een genre te initialiseren vanuit de database en de synchronizer te gebruiken
        public GenreViewModel(Genre genre, LocalDbContext context)
        {
            this.genre = genre;
            _context = context;
            _synchronizer = new Synchronizer(context);
        }


        //Genres laden vanuit de API    
        [RelayCommand]
        public async Task LoadGenres()
        {
            var genresFromApi = await _synchronizer.GetGenresFromApiAsync();
            Genres.Clear();
            foreach (var genre in genresFromApi)
            {
                Genres.Add(genre);
            }
        }

    }
}
