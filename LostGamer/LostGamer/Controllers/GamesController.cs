using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostGamer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LostGamer.Controllers
{
    public class GamesController : Controller
    {
        private readonly LostGamerContext _context;
        private UserManager<IdentityUser> _userManager;
        private IHostingEnvironment _webroot;

        public GamesController(LostGamerContext context, UserManager<IdentityUser> userManager, IHostingEnvironment webroot)
        {
            _context = context;
            _userManager = userManager;
            _webroot = webroot;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var lostGamerContext = _context.Games.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platforms).Include(g => g.Rating);
            return View(await lostGamerContext.ToListAsync());
        }

        // copy Index relable GameLibrary 
        public async Task<IActionResult> GameLibrary()
        {
            var lostGamerContext = _context.Games.Include(g => g.Category).Include(g => g.Company).Include(g => g.Platforms).Include(g => g.Rating);
            return View(await lostGamerContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platforms)
                .Include(g => g.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // Copy Details, paste then Relable Show now GET: Games/Show/5
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platforms)
                .Include(g => g.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // GET: Games/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Category");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName");
            ViewData["PlatformsId"] = new SelectList(_context.Platforms, "Id", "PlatformName");
            ViewData["RatingId"] = new SelectList(_context.Rating, "Id", "RatingNum");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameTitle,PlatformsId,CategoryId,RatingId,Synopsis,CompanyId")] Games games,
            IFormFile FilePhoto)
        {
            if (FilePhoto.Length > 0)
            {
                string coverLogo = _webroot.WebRootPath + "\\gamePhotos\\";
                var fileName = Path.GetFileName(FilePhoto.FileName);
                
                using (var stream = System.IO.File.Create(coverLogo + fileName))
                {
                    await FilePhoto.CopyToAsync(stream);
                    games.CoverLogo = fileName;
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(games);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Category", games.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName", games.CompanyId);
            ViewData["PlatformsId"] = new SelectList(_context.Platforms, "Id", "PlatformName", games.PlatformsId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "Id", "RatingNum", games.RatingId);
            return View(games);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Category", games.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName", games.CompanyId);
            ViewData["PlatformsId"] = new SelectList(_context.Platforms, "Id", "PlatformName", games.PlatformsId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "Id", "RatingNum", games.RatingId);
            return View(games);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameTitle,PlatformsId,CategoryId,RatingId,Synopsis,CompanyId")] Games games,
            IFormFile FilePhoto)
        {
            if (id != games.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Category", games.CategoryId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "CompanyName", games.CompanyId);
            ViewData["PlatformsId"] = new SelectList(_context.Platforms, "Id", "PlatformName", games.PlatformsId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "Id", "RatingNum", games.RatingId);
            return View(games);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Company)
                .Include(g => g.Platforms)
                .Include(g => g.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var games = await _context.Games.FindAsync(id);
            _context.Games.Remove(games);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
