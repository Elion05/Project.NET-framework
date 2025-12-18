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
            // Load books from the database on startup
            LoadBooks();
        }

        [ObservableProperty]
        ObservableCollection<MangaBook> mangaboeken;

        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        string description = string.Empty;

        private async void LoadBooks()
        {
            var books = await _context.MangaBooks.ToListAsync();
            Mangaboeken = new ObservableCollection<MangaBook>(books);
        }

        [RelayCommand]
        async Task VoegToe()
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
                AuthorId = 1, // Placeholder
                GenreId = 1   // Placeholder
            };

            _context.MangaBooks.Add(boek);
            await _context.SaveChangesAsync();
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
                    _context.MangaBooks.Remove(book);
                    await _context.SaveChangesAsync();
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
                        new MangaBookViewModel(book, _context)
                    ));
            }
        }

        [RelayCommand]
        async Task OpenBoekenPagina()
        {
            if (Application.Current?.MainPage is Page mainPage)
            {
                await mainPage.Navigation.PushAsync(
                    new MangaBookPage(new MangaBookViewModel(new MangaBook(), _context))
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
