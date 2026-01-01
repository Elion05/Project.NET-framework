using MangaBook_Models;
using Manga_App.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace Manga_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        readonly LocalDbContext _context;
        public MainViewModel(LocalDbContext context)
        {
            _context = context;
            LoadBooks();
            LoadAuthors();
            LoadGenres();
            LoadNieuwsBerichten();
        }

        [ObservableProperty]
        ObservableCollection<MangaBook> mangaboeken;

        [ObservableProperty]
        ObservableCollection<Author> authors;

        [ObservableProperty]
        ObservableCollection<Genre> genres;


        [ObservableProperty]
        ObservableCollection<Nieuws_Bericht> nieuwsBericht;

        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        string description = string.Empty;

        // Command voor navigatie naar boekenpagina
        [RelayCommand]
        async Task OpenBoekenPagina()
        {
            var vm = new MangaBookViewModel(new MangaBook(), _context);
            await Shell.Current.Navigation.PushAsync(new MangaBookPage(vm));
        }

        // Command voor navigatie naar auteurs pagina
        [RelayCommand]
        async Task OpenAuteursPagina()
        {
            var vm = new AuthorViewModel(new Author(), _context);
            await Shell.Current.Navigation.PushAsync(new AuthorPage(vm));
        }


        [RelayCommand]
        async Task OpenGenresPagina()
        {
            var vm = new GenreViewModel(new Genre(), _context);
            await Shell.Current.Navigation.PushAsync(new GenrePage(vm));
        }

        [RelayCommand]
        async Task OpenNieuwsBerichtenPagina()
        {
            var vm = new Nieuws_BerichtViewModel(new Nieuws_Bericht(), _context);
            await Shell.Current.Navigation.PushAsync(new Nieuws_BerichtPage(vm));
        }

        [RelayCommand]
        async Task GoToLogin()
        {
            if (Application.Current?.MainPage is Page mainPage)
            {
                await mainPage.Navigation.PushAsync(
                    new LoginPage(new LoginViewModel(_context), _context));
            }
        }

        private async void LoadBooks()
        {
            Synchronizer sync = new Synchronizer(_context);
            var boeken = await sync.GetMangaBooksFromApiAsync();

            if (boeken.Any())
                Mangaboeken = new ObservableCollection<MangaBook>(boeken);
            else
                Mangaboeken = new ObservableCollection<MangaBook>(
                    await _context.MangaBooks.ToListAsync());
        }


        private async void LoadAuthors()
        {
            Synchronizer sync = new Synchronizer(_context);
            var auteurs = await sync.GetAuthorsFromApiAsync();

            if (auteurs.Any())
                Authors = new ObservableCollection<Author>(auteurs);
            else
                Authors = new ObservableCollection<Author>(
                    await _context.Authors.ToListAsync());
        }



        private async void LoadGenres()
        {
            Synchronizer sync = new Synchronizer(_context);
            var genres = await sync.GetGenresFromApiAsync();
            if (genres.Any())
                Genres = new ObservableCollection<Genre>(genres);
            else
                Genres = new ObservableCollection<Genre>(
                    await _context.Genres.ToListAsync());
        }




        private async void LoadNieuwsBerichten()
        {
            Synchronizer sync = new Synchronizer(_context);
            var berichten = await sync.GetNieuwsBerichtenFromApiAsync();
            if (berichten.Any())
                NieuwsBericht = new ObservableCollection<Nieuws_Bericht>(berichten);
            else
                NieuwsBericht = new ObservableCollection<Nieuws_Bericht>(
                    await _context.Nieuws_Berichten.ToListAsync());
        }
    }
}