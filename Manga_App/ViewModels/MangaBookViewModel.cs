using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;


namespace Manga_App.ViewModels
{
    public partial class MangaBookViewModel : ObservableObject
    {
        [ObservableProperty]
        MangaBook mangaBook;

        readonly LocalDbContext _context;

        // Constructor updated to accept the context
        public MangaBookViewModel(MangaBook book, LocalDbContext context)
        {
            mangaBook = book;
            _context = context;
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



