using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;



namespace Manga_App.ViewModels
{
    public partial class MangaBookViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<MangaBook> mangaBoeken = new(); 
        MangaBook mangaBook;
        readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        //Constructor updated to accept the context
        public MangaBookViewModel(MangaBook book, LocalDbContext context)
        {
            mangaBook = book;
            _context = context;
            _synchronizer = new Synchronizer(context);
        }


        [RelayCommand]
        public async Task LoadMangaBooks()
        {
            var boeken = await _synchronizer.GetMangaBooksFromApiAsync();

            if (boeken.Any())
            {
                MangaBoeken.Clear();
                foreach (var boek in boeken)
                {
                    MangaBoeken.Add(boek);
                }
            }
               
        }






        [RelayCommand]
        async Task Save()
        {
            if (mangaBook == null || string.IsNullOrWhiteSpace(mangaBook.Title))
            {
                // TODO: Show an alert to the user
                return;
            }

            if (mangaBook.Id == 0)
            {
                // New book
                _context.MangaBooks.Add(mangaBook);
            }
            else
            {
                // Existing book
                _context.MangaBooks.Update(mangaBook);
            }

            await _context.SaveChangesAsync();

            // Navigate back
            if (Application.Current?.Windows[0].Page is Page mainPage)
            {
                await mainPage.Navigation.PopAsync();
            }
        }
    }
}



