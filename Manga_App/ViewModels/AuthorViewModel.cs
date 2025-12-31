using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Manga_App.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Author> authors = new();

        Author author;
        readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        

        public AuthorViewModel(Author author, LocalDbContext context)
        {
            this.author = author;
            _context = context;
            _synchronizer = new Synchronizer(context);
        }

       

        // Dit is de command om de auteurs te laden vanuit de API
        [RelayCommand]
        public async Task LoadAuthors()
        {
            var auteurs = await _synchronizer.GetAuthorsFromApiAsync();
            Authors.Clear();
            foreach (var auteur in auteurs)
            {
                Authors.Add(auteur);
            }
        }
    }
}
