using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace MangaBook_WPF
{
    //7) Algemene Gebruikersbeheer, admin kan gerbuikers blokkeren, en rollen toevoegen
    public partial class Gebruikersbeheer : Window
    {
        private readonly MangaDbContext _context;
        private readonly UserManager<MangaUser> _userManager;
        private MangaUser? _selectedUser;

        public Gebruikersbeheer(MangaDbContext context, UserManager<MangaUser> userManager)
        {
            InitializeComponent();
            _context = context;
            _userManager  = userManager;

            cbUsers.ItemsSource = _userManager.Users
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.UserName)
                .ToList();
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUsers.SelectedItem is not MangaUser selected)
                return;

            _selectedUser = selected;

            using var freshContext = new MangaDbContext();

            //User roles ophalen
            var userRoles = freshContext.UserRoles
                .Where(ur => ur.UserId == _selectedUser.Id)
                .Select(ur => ur.RoleId)
                .ToList();

            // Rollenlijst voorbereiden
            var roles = freshContext.Roles
                .Select(role => new ListBoxItem
                {
                    Content = role.Name,
                    Tag = role.Name, // Gebruik rolnaam, niet ID
                    IsSelected = userRoles.Contains(role.Id)
                })
                .ToList();

            lbRoles.ItemsSource = roles;
            lbRoles.Visibility = Visibility.Visible;
            btnBlokeer.Visibility = Visibility.Visible;

            btnBlokeer.Content = _selectedUser.LockoutEnd == null
                ? "Blokkeer gebruiker"
                : "Deblokkeer gebruiker";
        }


        //wanneer je een rol selecteert of deselecteert in de lijst, wordt de rol toegevoegd of verwijderd voor de geselecteerde gebruiker in de database, dus direct opgeslagen
        private async void lbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedUser == null)
                return;

            using var freshContext = new MangaDbContext();
            var userStore = new UserStore<MangaUser>(freshContext);
            var userManager = new UserManager<MangaUser>(
                userStore, null, new PasswordHasher<MangaUser>(),
                null, null, null, null, null, null
            );

            var user = await userManager.FindByIdAsync(_selectedUser.Id);
            if (user == null)
                return;

            foreach (ListBoxItem item in lbRoles.Items)
            {
                string role = item.Tag.ToString()!;
                bool NieuweRol = lbRoles.SelectedItems.Contains(item);

                //dit is een bool om te contrleren of de gebruiker de rol al heeft
                bool HeeftRole = await userManager.IsInRoleAsync(user, role);

                
                if (NieuweRol && !HeeftRole)
                    await userManager.AddToRoleAsync(user, role);
                else if (!NieuweRol && HeeftRole)
                    await userManager.RemoveFromRoleAsync(user, role);
            }

            await freshContext.SaveChangesAsync();
            MessageBox.Show("Rollen succesvol bijgewerkt!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void btnBlokeer_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedUser == null)
                return;

            var user = await _userManager.FindByIdAsync(_selectedUser.Id);
            if(user == null)
                return;

            //gebruikers blokkeren
            if(user.LockoutEnd == null)
            {
                user.LockoutEnd = DateTimeOffset.MaxValue;
                MessageBox.Show($"{user.UserName} is nu geblokkeerd, je kan hem altijd deblokkeren.", "Geblokkeerd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                user.LockoutEnd = null;
                MessageBox.Show($"{user.UserName} is nu gedeblokkeerd.", "Gedeblokkeerd", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            await _userManager.UpdateAsync(user);

            btnBlokeer.Content = user.LockoutEnd == null
                ? "Blokkeer gebruiker"
                : "Deblokkeer gebruiker";
        }
    }
}
