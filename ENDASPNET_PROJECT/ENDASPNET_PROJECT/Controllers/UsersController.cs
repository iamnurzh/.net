using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENDASPNET_PROJECT.Data;
using ENDASPNET_PROJECT.Models.Posts;
using ENDASPNET_PROJECT.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ENDASPNET_PROJECT.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        public UsersContext Context => _context;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var json = JsonConvert.SerializeObject(model);
            return Content(json);
        }
        public IActionResult ValidateUserId(int uid)
        {
            if (uid == 1)
                return Json(data: "Tut zanyato!)");

            return Json(data: true);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Role")]User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Context.Add(user);
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(user);
        }
        public async Task<IActionResult> Search(string text)
        {
            text = text.ToLower();
            var searchedUsers = await Context.Users.Where(users => users.Name.ToLower().Contains(text))
                                        .ToListAsync();
            return View("Index", searchedUsers);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userToUpdate = await Context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                s => s.Id, s => s.Name, s => s.Role))
            {
                try
                {
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(userToUpdate);
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await Context.Users.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(user);
        }
    }
}