using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data.DbContext;
using WebApplication3.Data.Models;
using WebApplication3.Data.Models.Cart;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        private StoreContext db;
        private ShopCart shopCart;

        public OrderController(StoreContext context, ShopCart cart)
        {
            db = context;
            shopCart = cart;
        }

        public IActionResult Checkout()
        {
            var cartItems = shopCart.GetShopItems();
            if (cartItems.Count == 0)
            {
                TempData["EmptyCartError"] = "Ваша корзина пуста! Добавьте товары в корзину перед оформлением заказа.";
                return RedirectToAction("Index", "ShopCart");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {

            if (ModelState.IsValid)
            {
                CreateOrder(order);
                return RedirectToAction("Complete");
            }
            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ обработан";
            return View();
        }

        private void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            db.OrderTable.Add(order);
            db.SaveChanges();

            var items = shopCart.ListShopItems;

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    CarId = el.Car.CarId,
                    OrderId = order.OrderId,
                    Price = el.Car.Price
                };
                db.OrderDetailTable.Add(orderDetail);
                db.SaveChanges();
            }
        }

    }
}
