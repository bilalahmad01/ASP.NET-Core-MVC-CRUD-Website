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
    }
}
