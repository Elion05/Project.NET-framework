using MangaBook_Models;
using System.Windows;


namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window
    {
        private readonly MangaDbContext _context;
        public AddAuthorWindow(MangaDbContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Auteurnaam = tbAuteurNaam.Text.Trim();

            if(string.IsNullOrWhiteSpace(Auteurnaam))
            {
                MessageBox.Show("Voer een geldige auteur naam in .");
                return;
            }

            if(_context.Authors.Any(a => a.Name.ToLower() == Auteurnaam.ToLower()))
            {
                MessageBox.Show("Deze auteur bestaat al.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newAuthor = new Author { Name = Auteurnaam };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            MessageBox.Show($"Auteur '{newAuthor.Name}' is succesvol toegevoegd.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
