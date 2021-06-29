using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db { get; set; }

        [System.Obsolete]
        private IHostingEnvironment _he;

        [System.Obsolete]
        public ProductController(ApplicationDbContext _db, IHostingEnvironment he)
        {
            db = _db;
            _he = he;
        }

        public IActionResult Index()
        {
            return View(db.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).ToList());
        }
        //Action Method to Create New Product
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagsId"] = new SelectList(db.SpecialTags.ToList(), "Id", "specialtagname");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {
            if (image != null)
            {
                var name = Path.Combine(_he.WebRootPath + "/Images",
                    Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                product.Image = "/Images/" + image.FileName;
            }
            if (image == null)
            {
                product.Image = "Images/noimage.PNG";
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // Action Method to Edit Products
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["ProductTypeId"] = new SelectList(db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagsId"] = new SelectList(db.SpecialTags.ToList(), "Id", "specialtagname");
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Products product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // Action Method to check Details of the Product
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                id = 2;
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["ProductTypeId"] = new SelectList(db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTagsId"] = new SelectList(db.SpecialTags.ToList(), "Id", "specialtagname");
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}