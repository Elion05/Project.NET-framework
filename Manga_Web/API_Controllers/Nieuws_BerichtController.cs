using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MangaBook_Models;

namespace Manga_Web.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Nieuws_BerichtController : ControllerBase
    {
        private readonly MangaDbContext _context;

        public Nieuws_BerichtController(MangaDbContext context)
        {
            _context = context;
        }

        // GET: api/Nieuws_Bericht
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nieuws_Bericht>>> GetNieuws_Berichten()
        {
            return await _context.Nieuws_Berichten.ToListAsync();
        }

        // GET: api/Nieuws_Bericht/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nieuws_Bericht>> GetNieuws_Bericht(int id)
        {
            var nieuws_Bericht = await _context.Nieuws_Berichten.FindAsync(id);

            if (nieuws_Bericht == null)
            {
                return NotFound();
            }

            return nieuws_Bericht;
        }

        // PUT: api/Nieuws_Bericht/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNieuws_Bericht(int id, Nieuws_Bericht nieuws_Bericht)
        {
            if (id != nieuws_Bericht.Id)
            {
                return BadRequest();
            }

            _context.Entry(nieuws_Bericht).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Nieuws_BerichtExists(id))
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

        // POST: api/Nieuws_Bericht
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nieuws_Bericht>> PostNieuws_Bericht([FromBody]Nieuws_Bericht nieuws_Bericht)
        {
            _context.Nieuws_Berichten.Add(nieuws_Bericht);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNieuws_Bericht", new { id = nieuws_Bericht.Id }, nieuws_Bericht);
        }

        // DELETE: api/Nieuws_Bericht/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNieuws_Bericht(int id)
        {
            var nieuws_Bericht = await _context.Nieuws_Berichten.FindAsync(id);
            if (nieuws_Bericht == null)
            {
                return NotFound();
            }

            _context.Nieuws_Berichten.Remove(nieuws_Bericht);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Nieuws_BerichtExists(int id)
        {
            return _context.Nieuws_Berichten.Any(e => e.Id == id);
        }
    }
}
