using MangaBook_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MangaBook_Models.Data;


namespace Manga_Web
{
   
    public class MangaBooksController : Controller
    {
        
        private readonly MangaDbContext _context;
        private readonly IStringLocalizer<MangaBooksController> _localizer;

        public MangaBooksController(MangaDbContext context, IStringLocalizer<MangaBooksController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // Vereiste: systematisch gebruik van asynchrone verwerking in de backend
        // OPDRACHT: Asynchrone verwerking in de backend wordt hier systematisch toegepast (async/await).
        // GET: MangaBooks
        public async Task<IActionResult> Index(string searchString, int? genreId, string sorteren)
        {

            
            ViewData["Authors"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["Genres"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentGenre"] = genreId;
            ViewData["Sorteren"] = sorteren;

            var mangaBooks =   from m in _context.MangaBooks.Include(m => m.Author).Include(m => m.Genre)
                             select m;

            // Vereiste: selectie- (filter-) en/of sorteringsvelden in je index-pagina's
            // OPDRACHT: Selectie- (filter-) en sorteringsvelden in de index-pagina.
            if (!String.IsNullOrEmpty(searchString))
            {
                mangaBooks = mangaBooks.Where(s => s.Title.Contains(searchString));
            }

            //dit is om de boek te sorteren op genre. als er geen is zie je een error message
            if(genreId.HasValue && genreId.Value != 0)
            {
              mangaBooks = mangaBooks.Where(m => m.GenreId == genreId.Value);
            }
            //dit is om te sorteren op datum en titel
            if (!string.IsNullOrEmpty(sorteren))
            {
                switch (sorteren)
                {
                    case "title":
                        mangaBooks = mangaBooks.OrderBy(m => m.Title);
                        break;
                    case "title_desc":
                        mangaBooks = mangaBooks.OrderByDescending(m => m.Title);
                        break;
                    case "date":
                        mangaBooks = mangaBooks.OrderBy(m => m.ReleaseDate);
                        break;
                    case "date_desc":
                        mangaBooks = mangaBooks.OrderByDescending(m => m.ReleaseDate);
                        break;
                    default:
                        mangaBooks = mangaBooks.OrderBy(m => m.Title);
                        break;
                }
            }
            else
            {
                mangaBooks = mangaBooks.OrderBy(m => m.Title);
            }

                var result = await mangaBooks.AsNoTracking().ToListAsync();

            if (result.Count == 0)
            {
                ViewData["ShowNoBooksMessage"] = true;
            }

            return View("Index", result);
        }

        // GET: MangaBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaBook = await _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mangaBook == null)
            {
                return NotFound();
            }

            return View(mangaBook);
        }

        // GET: MangaBooks/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();


        }

        // POST: MangaBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsDeleted,ReleaseDate,AuthorId,GenreId, AverageRating")] MangaBook mangaBook)
        {

            //dit is hoe we de userId kunnen ophalen uit jouw eigen Middleware
            string userId = (string)Request.HttpContext.Items["UserId"];

            if (ModelState.IsValid)
            {
                _context.Add(mangaBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", mangaBook.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", mangaBook.GenreId);
            return View(mangaBook);
        }



        // GET: MangaBooks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaBook = await _context.MangaBooks.FindAsync(id);
            if (mangaBook == null)
            {
                return NotFound();
            }

            // Vereiste: Je gebruikt een eigen middleware om de userId te bepalen
            string userId = (string)Request.HttpContext.Items["UserId"];

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", mangaBook.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", mangaBook.GenreId);
            return View(mangaBook);
        }


        //Dit is een systematische manier om asychrone wijzingen te maken in de backend via de EditBook view (PartialView)
        // Vereiste: asynchrone (Ajax) implementatie voor minstens één View
        // OPDRACHT: Asynchrone (Ajax) implementatie voor minstens één View.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, [Bind("Id, Title, Description, IsDeleted, ReleaseDate, AuthorId, GenreId, AverageRating")] MangaBook mangaBook)
        {
          if(id != mangaBook.Id)
          {
            return NotFound();
          }
          
          string userId = (string)Request.HttpContext.Items["UserId"];

            if (ModelState.IsValid)
            {
                try
                {
                    MangaBook existing = await _context.MangaBooks.FirstAsync(at => at.Id == id);
                    existing.Title = mangaBook.Title;
                    existing.Description = mangaBook.Description;
                    existing.IsDeleted = mangaBook.IsDeleted;
                    existing.ReleaseDate = mangaBook.ReleaseDate;
                    existing.AuthorId = mangaBook.AuthorId;
                    existing.GenreId = mangaBook.GenreId;
                    existing.AverageRating = mangaBook.AverageRating;
                    

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) { }
            }

            var bookToReturn = await _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == mangaBook.Id);

            if (bookToReturn == null)
            {
                return NotFound();
            }
            ViewData["Authors"] = new SelectList(_context.Authors, "Id", "Name", bookToReturn.AuthorId);
            ViewData["Genres"] = new SelectList(_context.Genres, "Id", "Name", bookToReturn.GenreId);

            return PartialView("EditBook" , bookToReturn);
        }

        //// POST: MangaBooks/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsDeleted,ReleaseDate,AuthorId,GenreId")] MangaBook mangaBook)
        //{
        //    if (id != mangaBook.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(mangaBook);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MangaBookExists(mangaBook.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", mangaBook.AuthorId);
        //    ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", mangaBook.GenreId);
        //    return View(mangaBook);
        //}



        // GET: MangaBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaBook = await _context.MangaBooks
                .Include(m => m.Author)
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mangaBook == null)
            {
                return NotFound();
            }

            return View(mangaBook);
        }

        // POST: MangaBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mangaBook = await _context.MangaBooks.FindAsync(id);
            if (mangaBook != null)
            {
                _context.MangaBooks.Remove(mangaBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
