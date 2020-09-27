using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostGamer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LostGamer.ViewModels;

namespace LostGamer.Controllers
{
    public class GuidesController : Controller
    {
        private readonly LostGamerContext _context;
        private UserManager<IdentityUser> _userManager;

        public GuidesController(LostGamerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Guides
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var lostGamerContext = _context.Guides.Include(g => g.Game).Include(g => g.UserProfiles);
            return View(await lostGamerContext.ToListAsync());
        }
        //copy Index, paste then relable UserGuides.
        public async Task<IActionResult> UserGuides(int? id)
        {
            var lostGamerContext = _context.Guides.Where(g => g.UserProfilesId == id).Include(g => g.Game).Include(g => g.UserProfiles);
            return View(await lostGamerContext.ToListAsync());
        }

        //copy Index, paste then relable GamesGuides.
        public async Task<IActionResult> GamesGuides(int? id)
        {

            var lostGamerContext = _context.Guides.Where(g => g.GameId == id).Include(g => g.Game).Include(g => g.UserProfiles);
            //var lostGamerContext = _context.Guides.Include(g => g.Game.Id == id).Include(g => g.UserProfiles);
            return View(await lostGamerContext.ToListAsync());
        }

        // GET: Guides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guides = await _context.Guides
                .Include(g => g.Game)
                .Include(g => g.UserProfiles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guides == null)
            {
                return NotFound();
            }

            return View(guides);
        }

        //copy Details, paste then relable Guide
        public async Task<IActionResult> Guide(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guides = await _context.Guides
                .Include(g => g.Game)
                .Include(g => g.UserProfiles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guides == null)
            {
                return NotFound();
            }

            return View(guides);
        }

        // GET: Guides/Create
        [Authorize]
        public IActionResult Create(int id)
        {
            string userID = _userManager.GetUserId(User);
            UserProfiles profile = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userID);
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "GameTitle");
            ViewBag.UserProfileId = profile.Id;
            return View();
        }

        // POST: Guides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuideTitle,GuideContent,DateSubmitted,LastUpdated,UserProfilesId,GameId")] Guides guides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Guide));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "GameTitle", guides.GameId);
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", guides.UserProfilesId);
            return View(guides);
        }

        // GET: Guides/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guides = await _context.Guides.FindAsync(id);
            if (guides == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "GameTitle", guides.GameId);
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", guides.UserProfilesId);
            return View(guides);
        }

        // POST: Guides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuideTitle,GuideContent,DateSubmitted,LastUpdated,UserProfilesId,GameId")] Guides guides)
        {
            if (id != guides.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guides);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuidesExists(guides.Id))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Id", "GameTitle", guides.GameId);
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", guides.UserProfilesId);
            return View(guides);
        }

        // GET: Guides/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guides = await _context.Guides
                .Include(g => g.Game)
                .Include(g => g.UserProfiles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guides == null)
            {
                return NotFound();
            }

            return View(guides);
        }

        // POST: Guides/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guides = await _context.Guides.FindAsync(id);
            _context.Guides.Remove(guides);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuidesExists(int id)
        {
            return _context.Guides.Any(e => e.Id == id);
        }
    }
}
