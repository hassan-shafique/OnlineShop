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
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }


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















    }
}
