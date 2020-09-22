using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LostGamer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LostGamer.Controllers
{
    public class CommentsController : Controller
    {
        private readonly LostGamerContext _context;
        private UserManager<IdentityUser> _userManager;

        public CommentsController(LostGamerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var lostGamerContext = _context.Comments.Include(c => c.UserProfiles).Include(g => g.Guides);
            return View(await lostGamerContext.ToListAsync());
        }

        // GET: Comments/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.UserProfiles)
                .Include(c => c.Guides)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // GET: Comments/Create
        [Authorize]
        public IActionResult Create(int id)
        {
            string userID = _userManager.GetUserId(User);
            UserProfiles profile = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userID);
            ViewBag.UserProfileId = profile.Id;
            ViewBag.GuideId = id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,DatePosted,UserProfilesId,GuideId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", comments.UserProfilesId);
            return View(comments);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", comments.UserProfilesId);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Comment,DatePosted,UserProfilesId,GuideId")] Comments comments)
        {
            if (id != comments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.Id))
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
            //ViewData["UserProfilesId"] = new SelectList(_context.UserProfiles, "Id", "DisplayName", comments.UserProfilesId);
            return View(comments);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.UserProfiles)
                .Include(c => c.Guides)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
