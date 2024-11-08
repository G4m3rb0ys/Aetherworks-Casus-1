using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;

namespace Aetherworks_Victuz.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VictuzActivitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VictuzActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VictuzActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VictuzActivity>>> GetVictuzActivities()
        {
            return await _context.VictuzActivities.ToListAsync();
        }

        // GET: api/VictuzActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VictuzActivity>> GetVictuzActivity(int id)
        {
            var victuzActivity = await _context.VictuzActivities.FindAsync(id);

            if (victuzActivity == null)
            {
                return NotFound();
            }

            return victuzActivity;
        }

        // PUT: api/VictuzActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVictuzActivity(int id, VictuzActivity victuzActivity)
        {
            if (id != victuzActivity.Id)
            {
                return BadRequest();
            }

            _context.Entry(victuzActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VictuzActivityExists(id))
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

        // POST: api/VictuzActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VictuzActivity>> PostVictuzActivity(VictuzActivity victuzActivity)
        {
            _context.VictuzActivities.Add(victuzActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVictuzActivity", new { id = victuzActivity.Id }, victuzActivity);
        }

        // DELETE: api/VictuzActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVictuzActivity(int id)
        {
            var victuzActivity = await _context.VictuzActivities.FindAsync(id);
            if (victuzActivity == null)
            {
                return NotFound();
            }

            _context.VictuzActivities.Remove(victuzActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VictuzActivityExists(int id)
        {
            return _context.VictuzActivities.Any(e => e.Id == id);
        }
    }
}
