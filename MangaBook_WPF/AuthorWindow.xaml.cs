using MangaBook_Models;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class AuthorWindow : Window
    {
        public AuthorWindow(Author author)
        {
            InitializeComponent();
            DataContext = author;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
