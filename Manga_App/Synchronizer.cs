using Manga_App.Pages;
using Manga_App.ViewModels;
using MangaBook_Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Manga_App
{


    internal class Synchronizer
    {
        HttpClient client;
        JsonSerializerOptions sOptions;
        internal bool dbExists = false;

        readonly LocalDbContext _context;

        internal Synchronizer(LocalDbContext context)
        {
            _context = context;

            client = new HttpClient();
            sOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }


        async Task AllMangaBooks()
        {
            // Synchronize local changes to API: Not yet implemented!
            foreach (MangaBook book in _context.MangaBooks)
            {
                if (book.Id < 0)  // Modified or new book
                {
                }
            }

            // Synchronize from API to local
            if (await IsAuthorized())
            {
                Uri uri = new Uri(General.ApiUrl + "MangaBooks");
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<MangaBook>? books = JsonSerializer.Deserialize<List<MangaBook>>(responseBody, sOptions);
                    if (books != null && books.Count > 0)
                    {
                        // This logic is complex and might need review based on desired sync behavior.
                        // For now, it attempts to update existing or add new books.
                        foreach (MangaBook book in books)
                        {
                            MangaBook? existingBook = await _context.MangaBooks.FirstOrDefaultAsync(b => b.Id == book.Id);
                            if (existingBook != null)
                            {
                                // Update existing book
                                _context.Entry(existingBook).CurrentValues.SetValues(book);
                            }
                            else
                            {
                                // Add new book
                                _context.MangaBooks.Add(book);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    // TODO: Log exception
                }
            }
        }


        internal async Task<bool> IsAuthorized()
        {
            if (!string.IsNullOrEmpty(General.UserId) && General.UserId.Length > 10)
                return true;

            Uri uri = new Uri(General.ApiUrl + "isauthorized");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                General.User = JsonSerializer.Deserialize<MangaUser>(responseBody, sOptions);
                General.UserId = General.User?.Id;
                return !string.IsNullOrEmpty(General.UserId) && General.UserId.Length > 10;
            }
            catch (Exception)
            {
                General.UserId = MangaUser.Dummy.Id;
                return false;
            }
        }

        internal async Task InitializeDb()
        {

            await _context.Database.MigrateAsync();


            if (!await _context.MangaUsers.AnyAsync())
            {
                _context.Languages.Add(new Language { Code = "en", Name = "English" });
                _context.Languages.Add(new Language { Code = "fr", Name = "français" });
                _context.Languages.Add(new Language { Code = "nl", Name = "Nederlands" });
                MangaUser user = new MangaUser { UserName = "-", Email = "(local)", FirstName = "Local", LastName = "User", LanguageCode = "nl" };
                _context.MangaUsers.Add(user);
                await _context.SaveChangesAsync();
                _context.Genres.Add(new Genre { Name = "?" });
                await _context.SaveChangesAsync();
            }

            // Set the counter for local IDs to a safe negative value
            if (await _context.MangaBooks.AnyAsync())
            {
                long minId = await _context.MangaBooks.MinAsync(a => a.Id);
                if (minId < 0)
                {
                    General.LocalIdCounter = minId - 1;
                }
            }

            dbExists = true;
        }

        internal async Task<bool> Login(LoginModel loginModel)
        {
            Uri uri = new Uri(General.ApiUrl + "login");

            try
            {
                var oldLogins = _context.LoginModels.Where(l => l.Username == loginModel.Username).ToList();
                _context.LoginModels.RemoveRange(oldLogins);
                await _context.SaveChangesAsync();

                string jsonString = JsonSerializer.Serialize(loginModel, sOptions);
                HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                string responseBody = await response.Content.ReadAsStringAsync();
                General.User = JsonSerializer.Deserialize<MangaUser>(responseBody, sOptions);
                General.UserId = General.User?.Id;

                if (!string.IsNullOrEmpty(General.UserId) && General.UserId.Length > 10)
                {
                    var user = await _context.MangaUsers.FirstOrDefaultAsync(u => u.Id == General.UserId);
                    if (user != null)
                    {
                        _context.MangaUsers.Remove(user);
                    }
                    if (General.User != null)
                    {
                        _context.MangaUsers.Add(General.User);
                    }

                    loginModel.ValidTill = DateTime.Now + TimeSpan.FromHours(1);
                    _context.LoginModels.Add(loginModel);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                General.UserId = MangaUser.Dummy.Id;
            }
            return false;
        }

        private async Task LoginToAPI()
        {
            if (await IsAuthorized())
                return;

            try
            {
                LoginModel? lm = await _context.LoginModels.FirstOrDefaultAsync(l => l.ValidTill > DateTime.Now || l.RememberMe);
                if (lm != null)
                {
                    if (await Login(lm))
                        return;
                }
            }
            catch (Exception)
            {
                // Open the login screen if no valid login is found
                if (Application.Current?.MainPage is Page mainPage)
                {
                    await mainPage.Navigation.PushAsync(new LoginPage(new LoginViewModel(_context), _context));
                }
            }
        }

        internal async Task SynchronizeAll()
        {
            await LoginToAPI();

            if (!string.IsNullOrEmpty(General.UserId) && General.UserId.Length > 10)
            {
                await AllMangaBooks();
            }
        }



        internal async Task<List<MangaBook>> GetMangaBooksFromApiAsync()
        {
            try
            {
                //if (!await IsAuthorized())
                //    return new List<MangaBook>();

                Uri uri = new Uri(General.ApiUrl + "MangaBooks");
                HttpResponseMessage response = await client.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    return new List<MangaBook>();

                string responseBody = await response.Content.ReadAsStringAsync();

                List<MangaBook>? boeken = JsonSerializer.Deserialize<List<MangaBook>>(responseBody, sOptions);


                Console.WriteLine($"Aantal boeken opgehaald: {boeken?.Count ?? 0}"); //testen of de boeken wel worden opgehaalt

                return boeken ?? new List<MangaBook>();
            }
            catch (Exception ex)
            {
                return new List<MangaBook>();
            }
        }




        public async Task SyncMangaBooksFromApiAsync()
        {
            try
            {
                List<MangaBook> apiBoeken = await GetMangaBooksFromApiAsync();

                if (apiBoeken.Count == 0)
                    return;

                foreach (var apiBook in apiBoeken)
                {
                    var existing = await _context.MangaBooks.FirstOrDefaultAsync(b => b.Id == apiBook.Id);
                    if (existing != null)
                    {
                        _context.Entry(existing).CurrentValues.SetValues(apiBook);
                    }
                    else
                    {
                        _context.MangaBooks.Add(apiBook);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //
            }
        }


    }
}
