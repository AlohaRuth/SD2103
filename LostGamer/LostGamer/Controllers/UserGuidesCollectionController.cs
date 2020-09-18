using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LostGamer.Models;
using LostGamer.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LostGamer.Controllers
{
    public class UserGuidesCollectionController : Controller
    {
        private readonly LostGamerContext _context;
        private UserManager<IdentityUser> _userManager;

        public UserGuidesCollectionController(LostGamerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Userguidecollection()
        {
            UserProfiles profile = _context.UserProfiles.FirstOrDefault(id =>
                id.UserAccountId == _userManager.GetUserId(User));

            UserCollectionViewModel userguidecollection = new UserCollectionViewModel();
            userguidecollection.userGuidesColletions = _context.UserGuidesColletion.Where(from => from.UserProfilesId == profile.Id).ToList();

            List<Guides> fromList = new List<Guides>();
            foreach (var g in userguidecollection.userGuidesColletions)
            {
                fromList.Add(_context.Guides.FirstOrDefault(from => from.Id == g.UserProfilesId));
            }

            userguidecollection.guides = fromList;
            return View(userguidecollection);
        }

        public IActionResult NewGuide(int id)
        {
            ViewBag.GameId = id;
            return View();
        }


    }
}
