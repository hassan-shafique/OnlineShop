using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        // Action method to Create new product type

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // Action method to Edit product type
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // Action method to show Details product type
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }
        // Action method to Delete product type
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var producttype = _db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return NotFound();
            }
            return View(producttype);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}
