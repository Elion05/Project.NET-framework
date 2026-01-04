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

        ObservableCollection<Genre> alleGenres;

        //readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        [ObservableProperty]
        string searchtt;

        //constructor om een genre te initialiseren vanuit de database en de synchronizer te gebruiken
        public GenreViewModel(Genre genres, LocalDbContext context)
        {
            genres = genres;
            //_context = context;
            _synchronizer = new Synchronizer(context);
            

            alleGenres = new ObservableCollection<Genre>(context.Genres.ToList());
            Genres = new ObservableCollection<Genre>(alleGenres);
        }


        //Genres laden vanuit de API    
        [RelayCommand]
        public async Task LoadGenres()
        {
            var genresFromApi = await _synchronizer.GetGenresFromApiAsync();


            if (genresFromApi.Any())
            {
                alleGenres.Clear();
                foreach(var genre in genresFromApi)
                {
                    alleGenres.Add(genre);
                }
                Genres = new ObservableCollection<Genre>(alleGenres);
            }
        }


        [RelayCommand]
        void Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                Genres = new ObservableCollection<Genre>(alleGenres);
            }
            else
            {
                //filteren van genres op basis van de naam
                Genres = new ObservableCollection<Genre>
                    (alleGenres.Where(g => g.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }
        }

    }
}
