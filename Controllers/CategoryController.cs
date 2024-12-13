using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.DbContext;
using WebApplication3.Data.Models;

namespace WebApplication3.Controllers
{
    public class CategoryController(StoreContext context) : Controller
    {
        readonly StoreContext db = context;

        public IActionResult CategoryList()
        {
            return View(db.CategoryTable.ToList());
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (category != null)
            {
                db.CategoryTable.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("CategoryList");
        }
        public IActionResult EditCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return View(db.CategoryTable.Find(categoryId));
            }
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (category != null)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("CategoryList");
        }

        public IActionResult DeleteCategory(int? categoryId)
        {
            if (categoryId != null)
            {
                var category = db.CategoryTable.Find(categoryId);
                if (category != null)
                {
                    db.CategoryTable.Remove(category);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("CategoryList");
        }

    }
}
