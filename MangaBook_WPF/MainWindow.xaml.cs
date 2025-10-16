using MangaBook_Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class MainWindow : Window
    {
        private MangaDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new MangaDbContext();
            MangaDbContext.Seeder(_context);
            LoadData();
        }

        //dit is om de data te laden in de datagrid
        private void LoadData()
        {
            var mangaList = _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .ToList();

            MangaGrid.ItemsSource = mangaList;
            cbGenre.ItemsSource = _context.Genres.ToList();
        }

        //dit is om een boek toe te voegen
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            grDetails.Visibility = Visibility.Visible;
            grDetails.DataContext = new MangaBook();
        }

        // dit is om een boek te bewerken
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MangaGrid.SelectedItem is MangaBook selected)
            {
                grDetails.Visibility = Visibility.Visible;
                grDetails.DataContext = selected;
            }
        }

        //dit is om een boek te verwijderen
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MangaGrid.SelectedItem is MangaBook selected)
            {
                if (MessageBox.Show($"Weet je zeker dat je '{selected.Title}' wilt verwijderen?",
                                    "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _context.MangaBooks.Remove(selected);
                    _context.SaveChanges();
                    LoadData();
                }
            }
        }

        //dit is om een boek op te slaan
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (grDetails.DataContext is MangaBook book)
            {
                //deze gedeelte tot lijn 89 is om de auteur toe te voegen als die nog niet bestaat
                if (!string.IsNullOrWhiteSpace(tbAuthor.Text))
                {
                    var bestaandeAuteur = _context.Authors.FirstOrDefault(a => a.Name == tbAuthor.Text.ToLower());

                    if (bestaandeAuteur != null)
                    {
                        book.AuthorId = bestaandeAuteur.Id;
                    }
                    else
                    {
                        var nieuwAuteur = new Author
                        {
                            Name = tbAuthor.Text,
                            geboorteDatum = "Onbekend",
                            description = "Momteneel niks"
                        };
                        _context.Authors.Add(nieuwAuteur);
                        _context.SaveChanges();
                        book.AuthorId = nieuwAuteur.Id;
                    }
                    ;


                    //dit is voor de boek zelf te updaten of toe te voegen
                    if (book.Id == 0)
                        _context.MangaBooks.Add(book);
                    else
                        _context.MangaBooks.Update(book);

                    _context.SaveChanges();
                    grDetails.Visibility = Visibility.Collapsed;
                    LoadData();
                }
            }
        }
    }
    }

