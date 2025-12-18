using MangaBook_Models;
using Manga_App.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Manga_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        readonly LocalDbContext _context;
        public MainViewModel(LocalDbContext context)
        {
            _context = context;
            mangaboeken = new ObservableCollection<MangaBook>();
        }

        [ObservableProperty]
        ObservableCollection<MangaBook> mangaboeken;

        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        string description = string.Empty;


        [RelayCommand]
        void VoegToe()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Description))
                return;

            MangaBook boek = new MangaBook
            {
                Title = Title,
                Description = Description,
                Created = DateTime.Now,
                ReleaseDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 1
            };

            Mangaboeken.Add(boek);

            Title = string.Empty;
            Description = string.Empty;
        }



        [RelayCommand]
        async Task Verwijder(MangaBook book)
        {
            if (book == null) return;

            if (Application.Current?.MainPage is Page mainPage)
            {
                bool confirm = await mainPage.DisplayAlert(
                    "Verwijder Manga",
                    $"Ben je zeker dat je '{book.Title}' wil verwijderen?",
                    "Ja",
                    "Nee");

                if (confirm)
                {
                    Mangaboeken.Remove(book);
                }
            }
        }


        [RelayCommand]
        async Task Bewerk(MangaBook book)
        {
            if (book == null) return;

            if (Application.Current?.MainPage is Page mainPage)
            {
                await mainPage.Navigation.PushAsync(
                    new MangaBookPage(
                        new MangaBookViewModel(book)
                    ));
            }
        }

        [RelayCommand]
        async Task OpenBoekenPagina()
        {
            if (Application.Current?.MainPage is Page mainPage)
            {
                await mainPage.Navigation.PushAsync(
                    new MangaBookPage(new MangaBookViewModel(new MangaBook()))
                    );
            }
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
    }
}
