using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace MangaBook_WPF
{
    public partial class MainWindow : Window
    {
        //_context om te communiceren met de database
        private MangaDbContext _context;


        //constructor van MainWindow
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
                .Where(m => !m.IsDeleted) //soft delete
                .ToList();

            MangaGrid.ItemsSource = mangaList;

            //om de comboboxen te vullen met de data uit de database
            cbGenre.ItemsSource = _context.Genres.ToList();
            tbAuthor.ItemsSource = _context.Authors.ToList();
        }


        //dit is om een boek toe te voegen
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            brDetails.Visibility = Visibility.Visible;
            grDetails.DataContext = new MangaBook();
            tbAuthor.Text = string.Empty; // Clear author text for new entry
        }

        //Dit is om de formulier in te vullen aandehand van de geselecteerde boek
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MangaGrid.SelectedItem is MangaBook selected)
            { 
                brDetails.Visibility = Visibility.Visible;
                grDetails.DataContext = selected;
            }
        }

        //dit is om een boek te verwijderen, met bevestiging
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MangaGrid.SelectedItem is MangaBook selected)
            {
                var result = MessageBox.Show($"Ben je zeker dat je  '{selected.Title}' wilt verwijderen?", "Verwijderen bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Stop);

                //als ja is gekozen wordt het boek verwijderd, geen soft delete maar hard delete
                if (result == MessageBoxResult.Yes)
                {
                    selected.IsDeleted = true;
                    _context.MangaBooks.Update(selected);
                    _context.SaveChanges();
                    LoadData();
                    brDetails.Visibility = Visibility.Collapsed;
                }
            }
        }

        //deze functie is om de wijzigingen van het boek op te slaan, en ook een nieuwe auteur toe te voegen als die nog niet bestaat(kopelt het boek aan de juiste auteur automatisch)
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (grDetails.DataContext is MangaBook book)
            {
                if (!string.IsNullOrWhiteSpace(tbAuthor.Text))
                {
                    var authorName = tbAuthor.Text.Trim();
                    var bestaandeAuteur = _context.Authors.FirstOrDefault(a => a.Name.ToLower() == authorName.ToLower());

                    if (bestaandeAuteur != null)
                    {
                        book.AuthorId = bestaandeAuteur.Id;
                    }
                    else
                    {
                        var nieuwAuteur = new Author
                        {
                            Name = tbAuthor.Text,
                            geboorteDatum = "",
                            description = "",
                            favoriteFood = "",
                            Nationaliteit = "",
                            FavorieteSport = ""
                        };
                        _context.Authors.Add(nieuwAuteur);
                        _context.SaveChanges();
                        book.AuthorId = nieuwAuteur.Id;
                    };

                    if (book.Id == 0)
                        _context.MangaBooks.Add(book);
                    else
                        _context.MangaBooks.Update(book);
                    _context.SaveChanges();
                    brDetails.Visibility = Visibility.Collapsed;
                    LoadData();
                }
            }
        }


        //Dit is voor de selectie verandering in de datagrid zodat de bewerk en verwijder knop alleen actief is als er een item geselecteerd is
        private void MangaGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnEdit.IsEnabled = MangaGrid.SelectedItem != null;
            btnDelete.IsEnabled = MangaGrid.SelectedItem != null;
        }

        //dit gaat automatisch filteren terwijl je intypt dus ik heb geen knop voorzien
        private void tbSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string zoekterm = tbSearch.Text.ToLower();

            //hier kan je kiezen op wat je wilt filteren voor de zoekbalk
            var gefilterdeManga = _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .Where(m => !m.IsDeleted &&
                            (m.Title.ToLower().Contains(zoekterm) ||
                             m.Description.ToLower().Contains(zoekterm) ||
                             (m.Author != null && m.Author.Name.ToLower().Contains(zoekterm)) ||
                             (m.Genre != null && m.Genre.Name.ToLower().Contains(zoekterm))))


                .ToList();

            MangaGrid.ItemsSource = gefilterdeManga;
        }


        private void btnLoginLogout_Click(object sender, RoutedEventArgs e)
        {
            //Als niemand is ingelogd, toon LoginWindow
            if (App.User == null || App.User == MangaUser.Dummy)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Owner = this; 
                this.Hide();
                loginWindow.ShowDialog();

                //als LoginWindow gesloten is, controleer of er een gebruiker is ingelogd
                if (App.User != null && App.User != MangaUser.Dummy)
                {
                    btnLoginLogout.Content = "Logout";
                    this.Show();
                    Window_Loaded(this, new RoutedEventArgs());
                }
                else
                {
                    this.Close(); 
                }
            }
            else
            {
                //Bevestiging voor uitloggen
                var result = MessageBox.Show("Weet je zeker dat je wilt uitloggen?", "Bevestig logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    App.User = MangaUser.Dummy;
                    btnLoginLogout.Content = "Login";
                    Window_Loaded(this, new RoutedEventArgs());

                    //hide MainWindow and show LoginWindow again
                    var loginWindow = new LoginWindow();
                    loginWindow.Owner = this;
                    this.Hide();
                    loginWindow.ShowDialog();

                    // After LoginWindow closes, decide what to do
                    if (App.User != null && App.User != MangaUser.Dummy)
                    {
                        btnLoginLogout.Content = "Logout";
                        this.Show(); 
                    }
                    else
                    {
                        this.Close(); 
                    }
                }
            }
        }



        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnLoginLogout.Content = (App.User != null && App.User != MangaUser.Dummy)
                //dit is zodat de knop verandert van content
                ? "Logout"
                : "Login";

            if (App.User != null && App.User != MangaUser.Dummy)
            {
                btnProfiel.Visibility = Visibility.Visible; //profiel tonen voor iedereen die ingelogd is

                var serviceProvider = App.ServiceProvider;
                if(serviceProvider == null)
                {
                    menuAdmin.Visibility = Visibility.Collapsed;
                    btnAdd.Visibility = Visibility.Collapsed;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                    return;
                }

                //als de gebruiker admin roles heeft, toon alle knoppen
                var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
                bool isAdmin = await userManager.IsInRoleAsync(App.User, "Admin");
                menuAdmin.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnAdd.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnEdit.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnDelete.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

            }
            else
            {
                //Als er geen gebruiker is ingelogd, verberg alle admin knoppen
                btnProfiel.Visibility = Visibility.Collapsed; 
                menuAdmin.Visibility = Visibility.Collapsed;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
         



        //Roles knop in mainwindow
        private void btnRoles_Click(object sender, RoutedEventArgs e)
        {
            
            var serviceProvider = App.ServiceProvider;
            if (serviceProvider == null)
            {
                MessageBox.Show("ServiceProvider niet beschikbaar.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var userManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<MangaUser>>();
            var Gebruikersbeheer = new Gebruikersbeheer(_context, userManager);
            Gebruikersbeheer.Owner = this;
            Gebruikersbeheer.ShowDialog();
        }

        private void GenreName_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink hyperlink && hyperlink.DataContext is MangaBook mangaBook)
            {
                if (mangaBook.Genre != null)
                {
                    var genreWindow = new DetailsGenreWindow(mangaBook.Genre, _context);     
                    genreWindow.Owner = this;
                    genreWindow.ShowDialog();
                    
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Geen beschrijving beschikbaar voor dit genre.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                }       
            }
        }
        private void AuthorName_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink hyperlink && hyperlink.DataContext is MangaBook mangaBook)
            {
                if (mangaBook.Author != null)
                {
                    var authorWindow = new AuthorWindow(mangaBook.Author, _context);
                    authorWindow.Owner = this;
                    authorWindow.ShowDialog();

                    LoadData(); //data refresh 
                }
                else
                {
                    MessageBox.Show("Geen beschrijving beschikbaar voor deze auteur.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        //genre toevoegen knop in mainwindow
        private void btnGenreToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var genreWindow = new AddGenreWindow(_context);
            genreWindow.Owner = this;
            genreWindow.ShowDialog();
            cbGenre.ItemsSource = _context.Genres.ToList();
        }

        private void btnAuthorToevoegen_Click(object sender, RoutedEventArgs e)
        {
            var AddAuthorWindow = new AddAuthorWindow(_context);
            AddAuthorWindow.Owner = this;
            AddAuthorWindow.ShowDialog();
            tbAuthor.ItemsSource = _context.Authors.ToList();
        }

       private void GebruikersBeheer_Click(object sender, RoutedEventArgs e)
        {
            btnRoles_Click(sender, e);
        }
        private void MenuItem_AddGenre_Click(object sender, RoutedEventArgs e)
        {
            btnGenreToevoegen_Click(sender, e);
        }
        private void MenuItem_AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            btnAuthorToevoegen_Click(sender, e);
        }

        private void btnProfiel_Click(object sender, RoutedEventArgs e)
        {
            //als er een gebruiker is ingelogd, open het profiel venster
            if (App.User != null && App.User != MangaUser.Dummy)
            {
                var serviceProvider = App.ServiceProvider;
                if (serviceProvider == null)
                {
                    MessageBox.Show("ServiceProvider niet beschikbaar.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
                var profileWindow = new GebruikersProfiel(_context, userManager);
                profileWindow.Owner = this;
                profileWindow.ShowDialog();
            }
        }

        private void nieuwsBericht_Click(object sender, RoutedEventArgs e)
        {
            var nieuwsWindow = new NieuwsWindow();
            nieuwsWindow.Owner = this;
            nieuwsWindow.ShowDialog();
        }
    }
}