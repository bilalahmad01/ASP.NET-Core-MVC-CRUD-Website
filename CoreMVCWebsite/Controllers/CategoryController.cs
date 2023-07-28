using CoreMVCWebsite.Data;
using CoreMVCWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCWebsite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context) {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> getCategories = _context.Categories.ToList();
            return View(getCategories);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())         // server-side validation
            {
                ModelState.AddModelError("Name", "Name and Display Order not be same");
            }
            
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                TempData["success"] = "Category has created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null) {
             return NotFound();
            }
            Category? catFromDB = _context.Categories.Find(id);   // Find() works on only primary key
            //Category catFromDB = _context.Categories.FirstOrDefault(c=>c.Id==id);   // It work on every property
            //Category catFromDB = _context.Categories.Where(c=>c.Id==id).FirstOrDefault();   // Same as FirstOrDefault but also can do more calculations
            if (catFromDB == null)
            {
                return NotFound();
            }
            return View(catFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            
            if (ModelState.IsValid)
            {
                _context.Categories.Update(model);     // It will update data base on Id which is present in that model.
                _context.SaveChanges();
                TempData["success"] = "Category has updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {

            Category? catFromDB = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (catFromDB == null)
            {
                return NotFound();
            }
             _context.Categories.Remove(catFromDB);     // It will Delete data which has got from DB.
             _context.SaveChanges();
              TempData["success"] = "Category has deleted successfully";
              return RedirectToAction("Index");
          
        }
    }
}
