using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using NuGet.ProjectModel;

namespace Aetherworks_Victuz.Controllers
{
    public class SuggestionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SuggestionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        // GET: Suggestions
        public async Task<IActionResult> Index()
        {
            int? currentUserId = null;

            string userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedUserId))
            {
                currentUserId = parsedUserId;
            }

            var suggestions = await _context.Suggestions
                .Include(s => s.User)
                .ThenInclude(u => u.Credential)
                .Select(s => new SuggestionViewModel
                {
                    Suggestion = s,
                    IsLiked = currentUserId.HasValue && _context.SuggestionLiked.Any(l => l.SuggestionId == s.Id && l.UserId == currentUserId.Value),
                    LikeCount = _context.SuggestionLiked.Count(l => l.SuggestionId == s.Id)
                })
                .ToListAsync();

            return View(suggestions);
        }

        // GET: Suggestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.User)
                .ThenInclude(u => u.Credential)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // GET: Suggestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suggestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                var identityUserId = _userManager.GetUserId(User);

                // Find the custom User entry that corresponds to the IdentityUser ID
                var user = await _context.User.FirstOrDefaultAsync(u => u.CredentialId == identityUserId);
                if (user == null)
                {
                    return NotFound("User not found in the database.");
                }

                // Assign the custom User ID to the suggestion
                suggestion.UserId = user.Id;

                // Add and save the suggestion
                _context.Add(suggestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suggestion);
        }

        // GET: Suggestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.User)
                .ThenInclude(u => u.Credential)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (suggestion == null)
            {
                return NotFound();
            }

            // Populate ViewData with UserNames and their IDs
            ViewData["UserName"] = new SelectList(_context.Users, "Id", "UserName", suggestion.UserId);

            return View(suggestion);
        }

        // POST: Suggestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Suggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing suggestion from the database
                    var existingSuggestion = await _context.Suggestions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (existingSuggestion == null)
                    {
                        return NotFound();
                    }

                    // Retain the existing UserId if it's not explicitly set in the new suggestion
                    suggestion.UserId = existingSuggestion.UserId;

                    _context.Update(suggestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Suggestions.Any(e => e.Id == suggestion.Id))
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
            return View(suggestion);
        }

        // GET: Suggestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.User)
                .ThenInclude(u => u.Credential)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // POST: Suggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suggestion = await _context.Suggestions.FindAsync(id);
            if (suggestion != null)
            {
                _context.Suggestions.Remove(suggestion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuggestionExists(int id)
        {
            return _context.Suggestions.Any(e => e.Id == id);
        }
    }
}