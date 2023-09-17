using CoreMVCWebsite.Data;
using CoreMVCWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace CoreMVCWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            List<Product> getProducts = _context.Products.Include(p => p.Category).ToList();
            return View(getProducts);
        }

        public IActionResult Upsert(int? id) {
            IEnumerable<SelectListItem> CategoryList = _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
            ViewBag.CategoryList = CategoryList;

            if (id==null || id==0)
            {   // create
                return View();
            }
            else
            {
                // update
                Product? ProductFromDB = _context.Products.Find(id);
                return View(ProductFromDB);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Product model, IFormFile? file)
        {

            //if (ModelState.IsValid)
            //{
                // upload image               
                string wwwRootPath = _environment.WebRootPath;       // Get path of wwwroot.
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);   // Path.GetExtension to save the original extension.
                    string productPath = Path.Combine(wwwRootPath, @"images\product");   // Get complete path of product folder.

                // IF update delete old image
                if(!string .IsNullOrEmpty(model.ImageUrl)) { 
                    var oldImagePath = Path.Combine(wwwRootPath, model.ImageUrl.TrimStart('\\'));  // Remove '\' from ImageUrl in DB.
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    model.ImageUrl = @"\images\product\" + fileName;
                }
                // upload image

            if(model.Id == 0)
            {
                   _context.Products.Add(model);
            }else
            {
                _context.Products.Update(model);
            }
               
                _context.SaveChanges();
                TempData["success"] = "Product has created successfully";
                return RedirectToAction("Index");

            //}
            //return View();
            
        }

        public IActionResult Delete(int? id)
        {

            Product? catFromDB = _context.Products.FirstOrDefault(c => c.Id == id);
            if (catFromDB == null)
            {
                return NotFound();
            }
             _context.Products.Remove(catFromDB);     // It will Delete data which has got from DB.
             _context.SaveChanges();
              TempData["success"] = "Product has deleted successfully";
              return RedirectToAction("Index");
          
        }
    }
}
