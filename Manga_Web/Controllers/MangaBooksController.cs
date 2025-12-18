using AspNetCore.Unobtrusive.Ajax;
using Humanizer.Localisation;
using MangaBook_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manga_Web.Middleware;
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

        // GET: MangaBooks
        public async Task<IActionResult> Index(string searchString)
        {

            
            ViewData["Authors"] = new SelectList(_context.Authors, "Id", "Name");
            ViewData["Genres"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["CurrentFilter"] = searchString;

            var mangaBooks = from m in _context.MangaBooks.Include(m => m.Author).Include(m => m.Genre)
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mangaBooks = mangaBooks.Where(s => s.Title.Contains(searchString));
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

            string userId = (string)Request.HttpContext.Items["UserId"];

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", mangaBook.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", mangaBook.GenreId);
            return View(mangaBook);
        }



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

        private bool MangaBookExists(int id)
        {
            return _context.MangaBooks.Any(e => e.Id == id);
        }   
    }
}
