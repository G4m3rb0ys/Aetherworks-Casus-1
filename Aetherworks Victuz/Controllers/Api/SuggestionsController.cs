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
    public class SuggestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SuggestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Suggestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suggestion>>> GetSuggestions()
        {
            return await _context.Suggestions.ToListAsync();
        }

        // GET: api/Suggestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suggestion>> GetSuggestion(int id)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);

            if (suggestion == null)
            {
                return NotFound();
            }

            return suggestion;
        }

        // PUT: api/Suggestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuggestion(int id, Suggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return BadRequest();
            }

            _context.Entry(suggestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuggestionExists(id))
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

        // POST: api/Suggestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suggestion>> PostSuggestion(Suggestion suggestion)
        {
            _context.Suggestions.Add(suggestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuggestion", new { id = suggestion.Id }, suggestion);
        }

        // DELETE: api/Suggestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuggestion(int id)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            _context.Suggestions.Remove(suggestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuggestionExists(int id)
        {
            return _context.Suggestions.Any(e => e.Id == id);
        }
    }
}
