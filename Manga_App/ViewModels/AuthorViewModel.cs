using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;
using System.Collections.ObjectModel;

namespace Manga_App.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Author> authors = new();
        Author author;

        //readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        ObservableCollection<Author> alleAuteurs;


        [ObservableProperty]
        string searchaa;



        public AuthorViewModel(Author authors, LocalDbContext context)
        {
            authors = authors;
            //_context = context;
            _synchronizer = new Synchronizer(context);

            alleAuteurs = new ObservableCollection<Author>(context.Authors.ToList());
            Authors = new ObservableCollection<Author>(alleAuteurs);


        }

       

        // Dit is de command om de auteurs te laden vanuit de API
        [RelayCommand]
        public async Task LoadAuthors()
        {
            var auteurs = await _synchronizer.GetAuthorsFromApiAsync();


            if (auteurs.Any())
            {
                alleAuteurs.Clear();
                foreach(var auteur in auteurs)
                {
                    alleAuteurs.Add(auteur);
                }

                Authors = new ObservableCollection<Author>(alleAuteurs);
            }
        }

        [RelayCommand]
        void Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                Authors = new ObservableCollection<Author>(alleAuteurs);
            }
            else
            {
                Authors = new ObservableCollection<Author>
                    (alleAuteurs.Where(a => a.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }
        }


    }
}

   