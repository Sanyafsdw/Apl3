using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data.DbContext;
using WebApplication3.Data.Models;
using WebApplication3.Data.ViewModels;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext db;
        private const int pageSize = 6;

        public HomeController(StoreContext context)
        {
            db = context;
        }

        public IActionResult Index(int catId = 0, int page = 1)
        {
            ViewBag.Title = "Главная страница";

            IQueryable<Car> carsQuery = db.CarTable;

            if (catId > 0)
            {
                carsQuery = carsQuery.Where(c => c.CategoryId == catId);
            }

            int totalItems = carsQuery.Count();

            int maxPage = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var cars = carsQuery
                .OrderBy(c => c.CarId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new HomeIndexViewModel
            {
                PageName = catId == 0 ? "Все автомобили" : "Автомобили по категории",
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                },
                Cars = cars,
                CurrentCategory = catId
            };

            return View(model);
        }
    }
}
