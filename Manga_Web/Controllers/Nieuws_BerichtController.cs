using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MangaBook_Models;

namespace Manga_Web
{
    public class Nieuws_BerichtController : Controller
    {
        private readonly MangaDbContext _context;

        public Nieuws_BerichtController(MangaDbContext context)
        {
            _context = context;
        }

        // GET: Nieuws_Bericht
        public async Task<IActionResult> Index()
        {
            var mangaDbContext = _context.Nieuws_Berichten.Include(n => n.Gebruiker);
            return View(await mangaDbContext.ToListAsync());
        }

        // GET: Nieuws_Bericht/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws_Bericht = await _context.Nieuws_Berichten
                .Include(n => n.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nieuws_Bericht == null)
            {
                return NotFound();
            }

            return View(nieuws_Bericht);
        }

        // GET: Nieuws_Bericht/Create
        public IActionResult Create()
        {
            
            ViewData["GebruikerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Nieuws_Bericht/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Inhoud,Datum,GebruikerId,isVerwijderd")] Nieuws_Bericht nieuws_Bericht)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nieuws_Bericht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Users, "Id", "Id", nieuws_Bericht.GebruikerId);
            return View(nieuws_Bericht);
        }

        // GET: Nieuws_Bericht/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws_Bericht = await _context.Nieuws_Berichten.FindAsync(id);
            if (nieuws_Bericht == null)
            {
                return NotFound();
            }
            ViewData["GebruikerId"] = new SelectList(_context.Users, "Id", "Id", nieuws_Bericht.GebruikerId);
            return View(nieuws_Bericht);
        }

        // POST: Nieuws_Bericht/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Inhoud,Datum,GebruikerId,isVerwijderd")] Nieuws_Bericht nieuws_Bericht)
        {
            if (id != nieuws_Bericht.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nieuws_Bericht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Nieuws_BerichtExists(nieuws_Bericht.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Users, "Id", "Id", nieuws_Bericht.GebruikerId);
            return View(nieuws_Bericht);
        }

        // GET: Nieuws_Bericht/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws_Bericht = await _context.Nieuws_Berichten
                .Include(n => n.Gebruiker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nieuws_Bericht == null)
            {
                return NotFound();
            }

            return View(nieuws_Bericht);
        }

        // POST: Nieuws_Bericht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuws_Bericht = await _context.Nieuws_Berichten.FindAsync(id);
            if (nieuws_Bericht != null)
            {
                _context.Nieuws_Berichten.Remove(nieuws_Bericht);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Nieuws_BerichtExists(int id)
        {
            return _context.Nieuws_Berichten.Any(e => e.Id == id);
        }
    }
}
