using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENDASPNET_PROJECT.Data;
using ENDASPNET_PROJECT.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENDASPNET_PROJECT.Controllers
{
    public class CommentsController : Controller
    {
        private readonly NewsContext _context;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("id,content,authorId,compostId")]Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(comment);
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
            return View(comment);
        }
        public async Task<IActionResult> Search(string text)
        {
            text = text.ToLower();
            var searchedComments = await _context.Comments.Where(comments => comments.commentContent.ToLower().Contains(text))
                                        .ToListAsync();
            return View("Index", searchedComments);
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
            var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(s => s.commentId == id);
            if (await TryUpdateModelAsync<Comment>(
                commentToUpdate,
                "",
                s => s.commentId, s => s.commentContent, s => s.commentAuthor, s => s.compostID))
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
            return View(commentToUpdate);
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

            var comment = await _context.Comments.AsNoTracking().FirstOrDefaultAsync(m => m.commentId == id);
            if (comment == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(comment);
        }

    }
}