using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.DbContext;
using WebApplication3.Data.Models;
using WebApplication3.Data.ViewModels;

namespace WebApplication3.Controllers
{
    public class CarController(StoreContext context, IWebHostEnvironment environment) : Controller
    {
        private readonly StoreContext _db = context;
        private readonly IWebHostEnvironment _env = environment;

        public IActionResult SingleCar(int carId)
        {
            var car = _db.CarTable.Find(carId);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        public IActionResult CarList()
        {
            var cars = _db.CarTable.Include(c => c.Category).ToList();
            return View(cars);
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            var categories = _db.CategoryTable.ToList();
            return View(new AddCarViewModel
            {
                Car = new Car(),
                Categories = categories
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string directoryPath = Path.Combine(_env.WebRootPath, "img");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, uploadedFile.FileName);
                car.Img = $"/img/{uploadedFile.FileName}";

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await uploadedFile.CopyToAsync(fileStream);
            }

            _db.CarTable.Add(car);
            await _db.SaveChangesAsync();

            return RedirectToAction("CarList");
        }

        [HttpGet]
        public IActionResult EditCar(int? carId)
        {
            if (carId == null)
            {
                return RedirectToAction("CarList");
            }

            var car = _db.CarTable.Find(carId);
            if (car == null)
            {
                return NotFound();
            }

            var categories = _db.CategoryTable.ToList();
            return View(new AddCarViewModel
            {
                Car = car,
                Categories = categories
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditCar(Car car, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string directoryPath = Path.Combine(_env.WebRootPath, "img");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, uploadedFile.FileName);
                car.Img = $"/img/{uploadedFile.FileName}";

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await uploadedFile.CopyToAsync(fileStream);
            }

            _db.Entry(car).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction("CarList");
        }

        public IActionResult DeleteCar(int? carId)
        {
            if (carId != null)
            {
                var car = _db.CarTable.Find(carId);
                if (car != null)
                {
                    _db.CarTable.Remove(car);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("CarList");
        }
    }
}
