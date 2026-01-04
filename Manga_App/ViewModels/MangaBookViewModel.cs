
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


        ObservableCollection<MangaBook> alleMangaBoeken;

        readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        [ObservableProperty]
        string searchText;

     

        //Constructor updated to accept the context
        public MangaBookViewModel(MangaBook book, LocalDbContext context)
        {
            mangaBook = book;
            _context = context;
            _synchronizer = new Synchronizer(context);


            //boeken halen uit de lokale database dankzij Synchronizer
            alleMangaBoeken = new ObservableCollection<MangaBook>(context.MangaBooks.ToList());
            MangaBoeken = new ObservableCollection<MangaBook>(alleMangaBoeken);
        }



        //dit is de command om de manga boeken te laden vanuit de API die in de Manga_Web zit
        [RelayCommand]
        public async Task LoadMangaBooks()
        {
            var boeken = await _synchronizer.GetMangaBooksFromApiAsync();

            if (boeken.Any())
            {
                alleMangaBoeken.Clear();
                foreach (var boek in boeken)
                {
                    alleMangaBoeken.Add(boek);
                }

                MangaBoeken = new ObservableCollection<MangaBook>(alleMangaBoeken);
            }
               
        }

        
        [RelayCommand]
        void Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                MangaBoeken = new ObservableCollection<MangaBook>(alleMangaBoeken);
            }
            else
            {
               //Boeken filteren op titel
                MangaBoeken = new ObservableCollection<MangaBook>(
                    alleMangaBoeken.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                );
            }
            
          
        }
    }
}





