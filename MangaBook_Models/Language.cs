using System.ComponentModel.DataAnnotations;

namespace MangaBook_Models
{
    public class Language
    {
        public static List<Language> Languages = new List<Language>();
        public static Language Dummy = null;
       
        public string Name { get; set; }

        [Key]
        public string Code { get; set; }
        public bool IsSystemLanguage { get; set; }
        public DateTime IsActive { get; set; }

        public static void Seeder(MangaDbContext context)
        {
            if (!context.Languages.Any())
            {
                context.Languages.AddRange(
                    new Language { Code = "-", Name = "Dummy", IsSystemLanguage = false, IsActive = DateTime.UtcNow },
                    new Language { Code = "en", Name = "English", IsSystemLanguage = true, IsActive = DateTime.UtcNow },
                    new Language { Code = "fr", Name = "Français", IsSystemLanguage = true, IsActive = DateTime.UtcNow },
                    new Language { Code = "nl", Name = "Nederlands", IsSystemLanguage = true, IsActive = DateTime.UtcNow },
                    new Language { Code = "de", Name = "Deutsch", IsSystemLanguage = false, IsActive = DateTime.UtcNow },
                    new Language { Code = "es", Name = "Español", IsSystemLanguage = false, IsActive = DateTime.UtcNow }
                );
                context.SaveChanges();
            }
            Languages = context.Languages.Where(l => l.IsActive < DateTime.Now).OrderBy(l => l.Name).ToList();
            Dummy = Languages.FirstOrDefault(l => l.Code == "-") ?? new Language { Code = "-", Name = "Dummy", IsSystemLanguage = false, IsActive = DateTime.UtcNow };
            return;
        }
    }
}
