using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENDASPNET_PROJECT.Data;
using ENDASPNET_PROJECT.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENDASPNET_PROJECT.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NewsContext _context;

     /*   public NewsContext(NewsContext dbContext)
        {
            _context = dbContext;
        }*/

        public async Task<IActionResult> Index()
        {
            //var categories = await _context.Categories.ToListAsync();
            return View(/*categories*/);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Category categories)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(categories);
                    await _context.SaveChangesAsync();
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
            return View(categories);
        }

        public async Task<IActionResult> Search(string text)
        {
            text = text.ToLower();
            var searchedCategories = await _context.Categories.Where(users => users.categoryName.ToLower().Contains(text))
                                        .ToListAsync();
            return View("Index", searchedCategories);
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
            var categoriesToUpdate = await _context.Categories.FirstOrDefaultAsync(s => s.categoryId == id);
            if (await TryUpdateModelAsync<Category>(
                categoriesToUpdate,
                "",
                s => s.categoryId, s => s.categoryName))
            {
                try
                {
                    await _context.SaveChangesAsync();
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
            return View(categoriesToUpdate);
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

            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.categoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(category);
        }

    }
}