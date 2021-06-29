using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private ApplicationDbContext db;
        public SpecialTagsController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.SpecialTags.ToList());
        }

        // Action Method to Create new Tag
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags )
        {
            if (ModelState.IsValid)
            {
                db.SpecialTags.Add(specialTags);
                await db.SaveChangesAsync();
                TempData["save"] = "Special tag has been added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        // Action Method to Edit Special Tags
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                NotFound();
            }
            var tags = db.SpecialTags.Find(id);
            if (tags == null)
            {
                return NotFound();
            }
            return View(tags);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTags tags)
        {
            if (ModelState.IsValid)
            {
                db.Update(tags);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        // Action Method to get details of Special tags
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tag = db.SpecialTags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTags specialTags)
        {
            return RedirectToAction(nameof(Index));
        }
        // Action Method to Delete Special tags
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tag = db.SpecialTags.Find(id);
            if (tag==null)
            {
                return NotFound();
            }
            return View(tag);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id,SpecialTags specialTags)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (specialTags.Id != id)
            {
                return NotFound();
            }
            var tag = db.SpecialTags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                db.Remove(tag);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
