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
    public class UserProfilesController : Controller
    {
        private readonly LostGamerContext _context;
        private UserManager<IdentityUser> _userManager;

        public UserProfilesController(LostGamerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserProfiles
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var lostGamerContext = _context.UserProfiles.Include(u => u.UserType);
            return View(await lostGamerContext.ToListAsync());
        }

        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfiles = await _context.UserProfiles
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfiles == null)
            {
                return NotFound();
            }

            return View(userProfiles);
        }

        // GET: UserProfiles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName");
            return View();
        }

        public IActionResult ProfileInfo()
        {
            string userID = _userManager.GetUserId(User);
            UserProfiles profile = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userID);

            if (profile == null)
            {
                return RedirectToAction("Create");
            }

            return View(profile);

        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DisplayName,DateCreated,UserTypeId,UserAccountId")] UserProfiles userProfiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProfiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProfileInfo));
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName", userProfiles.UserTypeId);
            return View(userProfiles);
        }

        // GET: UserProfiles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfiles = await _context.UserProfiles.FindAsync(id);
            if (userProfiles == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName", userProfiles.UserTypeId);
            return View(userProfiles);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfiles = await _context.UserProfiles.FindAsync(id);
            if (userProfiles == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName", userProfiles.UserTypeId);
            return View(userProfiles);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DisplayName,DateCreated,UserTypeId,UserAccountId")] UserProfiles userProfiles)
        {
            if (id != userProfiles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfilesExists(userProfiles.Id))
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
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName", userProfiles.UserTypeId);
            return View(userProfiles);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, [Bind("Id,FirstName,LastName,DisplayName,DateCreated,UserTypeId,UserAccountId")] UserProfiles userProfiles)
        {
            if (id != userProfiles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfilesExists(userProfiles.Id))
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
            ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "TypeName", userProfiles.UserTypeId);
            return View(userProfiles);
        }

        // GET: UserProfiles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfiles = await _context.UserProfiles
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfiles == null)
            {
                return NotFound();
            }

            return View(userProfiles);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfiles = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(userProfiles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfilesExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id);
        }
    }
}
