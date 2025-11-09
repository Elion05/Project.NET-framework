using MangaBook_Models;
using System.Linq;
using System.Windows;

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for AddGenreWindow.xaml
    /// </summary>
    public partial class AddGenreWindow : Window
    {
        private readonly MangaDbContext _context;

        public AddGenreWindow(MangaDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            string genreNaam = tbGenreName.Text.Trim();

            if (string.IsNullOrWhiteSpace(genreNaam))
            {
                MessageBox.Show("Voer een geldige genrenaam in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (genreNaam.Length > 20)
            {
                MessageBox.Show("De naam van het genre mag maximaal 20 karakters lang zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //controlleer of er al een genre bestaat met dezelfde naam, ongeacht de lettertype
            if (_context.Genres.Any(g => g.Name.ToLower() == genreNaam.ToLower()))
            {
                MessageBox.Show("Dit genre bestaat al.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Maak een nieuw genre aan met een standaard beschrijving
            var newGenre = new Genre
            {
                Name = genreNaam,
                genreBeschrijving = "Nog geen beschrijving beschikbaar."
            };

            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            MessageBox.Show($"Genre '{newGenre.Name}' is succesvol toegevoegd.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        //sluit window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
