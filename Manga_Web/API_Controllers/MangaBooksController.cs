using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MangaBook_Models;
using MangaBook_Models.NewFolder;

namespace Manga_Web.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaBooksController : ControllerBase
    {
        private readonly MangaDbContext _context;

        public MangaBooksController(MangaDbContext context)
        {
            _context = context;
        }

        // GET: api/MangaBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MangaBook>>> GetMangaBooks()
        {
            return await _context.MangaBooks.ToListAsync();
        }

        // GET: api/MangaBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MangaBook>> GetMangaBook(int id)
        {
            var mangaBook = await _context.MangaBooks.FindAsync(id);

            if (mangaBook == null)
            {
                return NotFound();
            }

            return mangaBook;
        }

        // PUT: api/MangaBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMangaBook(int id, MangaBook mangaBook)
        {
            if (id != mangaBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(mangaBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MangaBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MangaBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MangaBook>> PostMangaBook(MangaBook mangaBook)
        {
            _context.MangaBooks.Add(mangaBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMangaBook", new { id = mangaBook.Id }, mangaBook);
        }

        // DELETE: api/MangaBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMangaBook(int id)
        {
            var mangaBook = await _context.MangaBooks.FindAsync(id);
            if (mangaBook == null)
            {
                return NotFound();
            }

            _context.MangaBooks.Remove(mangaBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MangaBookExists(int id)
        {
            return _context.MangaBooks.Any(e => e.Id == id);
        }
    }
}
