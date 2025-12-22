using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MangaBook_Models;
using MangaBook_Models.Data;
using Microsoft.AspNetCore.Authorization;

namespace Manga_Web.Controllers
{

    [Authorize(Roles = "Admin, System_Admin")]
    public class LogErrorsController : Controller
    {
        private readonly MangaDbContext _context;

        public LogErrorsController(MangaDbContext context)
        {
            _context = context;
        }

        // GET: LogErrors
        public async Task<IActionResult> Index()
        {
            return View(await _context.LogErrors.ToListAsync());
        }

        // GET: LogErrors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logError = await _context.LogErrors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logError == null)
            {
                return NotFound();
            }

            return View(logError);
        }

        // GET: LogErrors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogErrors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,DeviceName,Application,LogLevel,ThreadId,EventId,EventName,ExceptionMessage,StackTrace,Source,Message")] LogError logError)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logError);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logError);
        }

        // GET: LogErrors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logError = await _context.LogErrors.FindAsync(id);
            if (logError == null)
            {
                return NotFound();
            }
            return View(logError);
        }

        // POST: LogErrors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStamp,DeviceName,Application,LogLevel,ThreadId,EventId,EventName,ExceptionMessage,StackTrace,Source,Message")] LogError logError)
        {
            if (id != logError.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logError);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogErrorExists(logError.Id))
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
            return View(logError);
        }

        // GET: LogErrors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logError = await _context.LogErrors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logError == null)
            {
                return NotFound();
            }

            return View(logError);
        }

        // POST: LogErrors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logError = await _context.LogErrors.FindAsync(id);
            if (logError != null)
            {
                _context.LogErrors.Remove(logError);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogErrorExists(int id)
        {
            return _context.LogErrors.Any(e => e.Id == id);
        }
    }
}
