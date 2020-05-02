using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENDASPNET_PROJECT.Data;
using ENDASPNET_PROJECT.Models.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ENDASPNET_PROJECT.Controllers
{
    public class PostsController : Controller
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
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("id,title,content,author,category")]Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(post);
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
            return View(post);
        }
        public async Task<IActionResult> Search(string text)
        {
            text = text.ToLower();
            var searchedPosts = await _context.Posts.Where(posts => posts.postTitle.ToLower().Contains(text)).ToListAsync();
            return View("Index", searchedPosts);
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
            var postToUpdate = await _context.Posts.FirstOrDefaultAsync(s => s.postId == id);
            if (await TryUpdateModelAsync<Post>(
                postToUpdate,
                "",
                s => s.postId, s => s.postTitle, s => s.postContent, s => s.postAuthor, s => s.postCategory))
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
            return View(postToUpdate);
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

            var post = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(m => m.postId == id);
            if (post == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(post);
        }
    }
}