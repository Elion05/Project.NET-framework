using MangaBook_Models;
using System.Windows;
using System.Linq;
using MangaBook_Models.NewFolder;


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
            string auteurnaam = tbAuteurNaam.Text.Trim();

            if (string.IsNullOrWhiteSpace(auteurnaam))
            {
                //15) Foutmeldingen enof waarschuwingen
                MessageBox.Show("Voer een geldige auteur naam in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (auteurnaam.Length > 30)
            {
                MessageBox.Show("De naam van de auteur mag maximaal 30 karakters lang zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_context.Authors.Any(a => a.Name.ToLower() == auteurnaam.ToLower()))
            {
                MessageBox.Show("Deze auteur bestaat al.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Maakt een nieuwe auteur aan met default values
            var newAuthor = new Author
            {
                Name = auteurnaam,
                geboorteDatum = "01/01/1900", 
                Nationaliteit = "ALBANEES" ,
                FavorieteSport = "Lekker niksen",
                favoriteFood = "BURGERSSSSSS JAAAA",
                description = "Nog geen beschrijving beschikbaar." 
            };

            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            MessageBox.Show($"Auteur '{newAuthor.Name}' is succesvol toegevoegd.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
