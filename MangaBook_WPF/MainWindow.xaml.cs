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
            brDetails.Visibility = Visibility.Visible;
            grDetails.DataContext = new MangaBook();
            tbAuthor.Text = string.Empty; // Clear author text for new entry
        }

        // dit is om een boek te bewerken en de details te tonen
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (MangaGrid.SelectedItem is MangaBook selected)
            {
                brDetails.Visibility = Visibility.Visible;
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
                    brDetails.Visibility = Visibility.Collapsed;
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
                            geboorteDatum = "Onbekend",
                            description = "Momteneel niks",
                            favoriteFood = "Onbekend",
                            Nationaliteit = "Onbekend",
                            FavorieteSport = "Onbekend"
                        };
                        _context.Authors.Add(nieuwAuteur);
                        _context.SaveChanges();
                        book.AuthorId = nieuwAuteur.Id;
                    };
                    //dit is voor de boek zelf te updaten of toe te voegen
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



        //Dit is de code voor de zoekbalk
        //dit gaat automatisch filteren terwijl je intypt dus ik heb geen knop voorzien
        private void tbSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            string zoekterm = tbSearch.Text.ToLower();

            //hier kan je kiezen op wat je wilt filteren,
            //dus je schrijft gewoon welke genre auteur of boek je wilt en het filtert daarop
            var gefilterdeManga = _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .Where(m =>
                    m.Title.ToLower().Contains(zoekterm) ||
                    (m.Author != null && m.Author.Name.ToLower().Contains(zoekterm)) ||
                    (m.Genre != null && m.Genre.Name.ToLower().Contains(zoekterm)) ||
                    //je kan nu op jaar zoeken
                    m.ReleaseDate.Year.ToString().Contains(zoekterm))

                .ToList();

            MangaGrid.ItemsSource = gefilterdeManga;
        }

        private void btnLoginLogout_Click(object sender, RoutedEventArgs e)
        {
            // Als niemand is ingelogd, toon LoginWindow
            if (App.User == null || App.User == MangaUser.Dummy)
            {
                var loginWindow = new LoginWindow();
                loginWindow.Owner = this; // Set owner to handle closing logic
                this.Hide();
                loginWindow.ShowDialog();

                // After LoginWindow closes, decide whether to show MainWindow or close it
                if (App.User != null && App.User != MangaUser.Dummy)
                {
                    btnLoginLogout.Content = "Logout";
                    this.Show();
                    Window_Loaded(this, new RoutedEventArgs());
                }
                else
                {
                    this.Close(); // Close MainWindow if login was not successful
                }
            }
            else
            {
                // Bevestiging voor uitloggen
                var result = MessageBox.Show("Weet je zeker dat je wilt uitloggen?", "Bevestig logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    App.User = MangaUser.Dummy;
                    btnLoginLogout.Content = "Login";
                    Window_Loaded(this, new RoutedEventArgs());

                    // Hide MainWindow and show LoginWindow again
                    var loginWindow = new LoginWindow();
                    loginWindow.Owner = this;
                    this.Hide();
                    loginWindow.ShowDialog();

                    // After LoginWindow closes, decide what to do
                    if (App.User != null && App.User != MangaUser.Dummy)
                    {
                        btnLoginLogout.Content = "Logout";
                        this.Show(); // User logged in again
                    }
                    else
                    {
                        this.Close(); // User did not log in, close the app
                    }
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnLoginLogout.Content = (App.User != null && App.User != MangaUser.Dummy)
                ? "Logout"
                : "Login";

            if (App.User != null && App.User != MangaUser.Dummy)
            {
                var serviceProvider = App.ServiceProvider;
                if (serviceProvider == null)
                {
                    //die knoppen verbergen als de serviceprovider niet beschikbaar is(copilot heeft dit toegevoegd)
                    btnRoles.Visibility = Visibility.Collapsed;
                    btnAdd.Visibility = Visibility.Collapsed;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                    btnGenreToevoegen.Visibility = Visibility.Collapsed;
                    return;
                }

                //Controleer of de gebruiker een Admin is, zo niet krijgt hij dit niet te zien
                var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
                bool isAdmin = await userManager.IsInRoleAsync(App.User, "Admin");
                btnRoles.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnAdd.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnEdit.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnDelete.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnGenreToevoegen.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

                if (!isAdmin)
                {
                    //worden niet actief als de gebruiker geen admin is
                    btnEdit.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                }
            }
            else
            {
                //Als er geen gebruiker is ingelogd, verberg alle admin knoppen
                btnRoles.Visibility = Visibility.Collapsed;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnGenreToevoegen.Visibility = Visibility.Collapsed;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
         
        
        //registratie knop in mainwindow
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var serviceProvider = App.ServiceProvider;
            if (serviceProvider == null)
            {
                MessageBox.Show("ServiceProvider niet beschikbaar.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var userManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<MangaUser>>();
            var registratieWindow = new RegistratieWindow(_context, userManager);
            registratieWindow.Owner = this;
            this.Hide();
            registratieWindow.ShowDialog();
            // After RegistratieWindow closes, show MainWindow again
            this.Show();

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
            var rolesWindow = new RolesWindow(_context, userManager);
            rolesWindow.Owner = this;
            rolesWindow.ShowDialog();
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
    }
}