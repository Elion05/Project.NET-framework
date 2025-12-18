using Microsoft.EntityFrameworkCore;

namespace MangaBook_Models
{
    public class LocalDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }

        public DbSet<MangaBook> MangaBooks { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<MangaUser> MangaUsers { get; set; }

        public DbSet<LoginModel> LoginModels { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "Manga_App.db");
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
