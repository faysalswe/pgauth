using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokenAuthWithPG.Data;
using TokenAuthWithPG.Model;

namespace TokenAuthWithPG.Controllers
{
    [Produces("application/json")]
    [Route("api/Sims")]
    public class SimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sims
        [HttpGet]
        public IEnumerable<Sim> GetSim()
        {
            return _context.Sims;
        }

        // GET: api/Sims/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSim([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sim = await _context.Sims.SingleOrDefaultAsync(m => m.Number == id);

            if (sim == null)
            {
                return NotFound();
            }

            return Ok(sim);
        }

        // PUT: api/Sims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSim([FromRoute] string id, [FromBody] Sim sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sim.Number)
            {
                return BadRequest();
            }

            _context.Entry(sim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return Ok( sim.Number + " Updated sucessfully");
        }

        // POST: api/Sims
        [HttpPost]
        public async Task<IActionResult> PostSim([FromBody] Sim sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sims.Add(sim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSim", new { id = sim.Number }, sim);
        }

        // DELETE: api/Sims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSim([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sim = await _context.Sims.SingleOrDefaultAsync(m => m.Number == id);
            if (sim == null)
            {
                return NotFound();
            }

            _context.Sims.Remove(sim);
            await _context.SaveChangesAsync();

            return Ok(sim);
        }

        private bool SimExists(string id)
        {
            return _context.Sims.Any(e => e.Number == id);
        }
    }
}