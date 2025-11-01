using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace MangaBook_WPF
{
    public partial class RolesWindow : Window
    {
        private readonly MangaDbContext _context;
        private readonly UserManager<MangaUser> _userManager;
        private MangaUser? _selectedUser;

        public RolesWindow(MangaDbContext context, UserManager<MangaUser> userManager)
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

            // User roles ophalen
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
        }

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
                bool shouldHaveRole = lbRoles.SelectedItems.Contains(item);

                bool hasRole = await userManager.IsInRoleAsync(user, role);

                if (shouldHaveRole && !hasRole)
                    await userManager.AddToRoleAsync(user, role);
                else if (!shouldHaveRole && hasRole)
                    await userManager.RemoveFromRoleAsync(user, role);
            }

            await freshContext.SaveChangesAsync();
            MessageBox.Show("Rollen succesvol bijgewerkt!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
