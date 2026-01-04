using MangaBook_Models;
using MangaBook_Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga_Web
{
    
    public class AuthorsController : Controller
    {
        private readonly MangaDbContext _context;
        private readonly IStringLocalizer<AuthorsController> _localizer;

        public AuthorsController(MangaDbContext context, IStringLocalizer<AuthorsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: Authors
        public async Task<IActionResult> Index(string searchString, string sorteerAuteur)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sorteerAuteur;

            var authors = from m in _context.Authors
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(s => s.Name.Contains(searchString));
            }

            //Sorteer op geboortedatum
            switch (sorteerAuteur)
            {
                case "date_desc":
                    authors = authors.OrderByDescending(a => a.geboorteDatum);
                    break;
                case "date":
                default:
                    authors = authors.OrderBy(a => a.geboorteDatum);
                    break;
            }

            var result = await authors.AsNoTracking().ToListAsync();
            if (result.Count == 0)
            {
                ViewData["ShowNoAuthorsMessage"] = true;
            }
            return View("Index", result);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,geboorteDatum,description,favoriteFood,Nationaliteit,FavorieteSport")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        //// GET: Authors/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var author = await _context.Authors.FindAsync(id);
        //    if (author == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(author);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuthor(int id, [Bind("Id,Name,geboorteDatum,description,favoriteFood,Nationaliteit,FavorieteSport")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Author existing = _context.Authors.First(at => at.Id == id);
                    existing.Name = author.Name;
                    existing.geboorteDatum = author.geboorteDatum;
                    existing.description = author.description;
                    existing.favoriteFood = author.favoriteFood;
                    existing.Nationaliteit = author.Nationaliteit;
                    existing.FavorieteSport = author.FavorieteSport;

                    _context.Update(existing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    
                }
           
            }
            return PartialView("EditAuthor", author);
        }





        //// POST: Authors/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,geboorteDatum,description,favoriteFood,Nationaliteit,FavorieteSport")] Author author)
        //{
        //    if (id != author.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(author);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AuthorExists(author.Id))
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
        //    return View(author);
        //}

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
