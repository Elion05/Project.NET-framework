using MangaBook_Models;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class AuthorWindow : Window
    {
        private readonly MangaDbContext _context;
        private readonly Author _author;
        public AuthorWindow(Author author, MangaDbContext context)
        {
            InitializeComponent();
            DataContext = author;
            _author = author;
            _context = context;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            brEditAuthorDetails.Visibility = Visibility.Visible;
            var author = DataContext as Author;
            if(author != null)
            {
                tbEditName.Text = author.Name;
                tbEditGeboorteDatum.Text = author.geboorteDatum;
                tbEditDescription.Text = author.description;
                tbEditFavoriteFood.Text = author.favoriteFood;
                tbEditNationaliteit.Text = author.Nationaliteit;
                tbEditFavorieteSport.Text = author.FavorieteSport;

            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_author != null)
            {
                _author.Name = tbEditName.Text;
                _author.geboorteDatum = tbEditGeboorteDatum.Text;
                _author.description = tbEditDescription.Text;
                _author.favoriteFood = tbEditFavoriteFood.Text;
                _author.Nationaliteit = tbEditNationaliteit.Text;
                _author.FavorieteSport = tbEditFavorieteSport.Text;

                _context.SaveChanges();
            }

            brEditAuthorDetails.Visibility = Visibility.Collapsed;

            DataContext = null;
            DataContext = _author;

            MessageBox.Show("Auteur informatie gewijzigd en opgeslagen.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
