using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MangaBook_Models;
using AspNetCore.Unobtrusive.Ajax;


namespace Manga_Web
{
    public class MangaBooksController : Controller
    {
        private readonly MangaDbContext _context;

        public MangaBooksController(MangaDbContext context)
        {
            _context = context;
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
                ViewData["Message"] = "We haven't found this book or it is not here yet.";
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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsDeleted,ReleaseDate,AuthorId,GenreId")] MangaBook mangaBook)
        {
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
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "Name", mangaBook.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", mangaBook.GenreId);
            return View(mangaBook);
        }



       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, [Bind("Id, Title, Description, IsDeleted, ReleaseDate, AuthorId, GenreId")] MangaBook mangaBook)
        {
          if(id != mangaBook.Id)
          {
            return NotFound();
          }

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
