using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data.DbContext;
using WebApplication3.Data.Models.Cart;

namespace WebApplication3.Controllers
{
    public class ShopCartController : Controller
    {
        StoreContext db;
        ShopCart _shopCart;

        public ShopCartController(StoreContext context, ShopCart shopCart)
        {
            db = context;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            _shopCart.ListShopItems = _shopCart.GetShopItems();
            return View(_shopCart);
        }

        public RedirectToActionResult AddToCart(int id)
        {
            var item = db.CarTable.Find(id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int id)
        {
            _shopCart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cartItems = _shopCart.GetShopItems();
            if (cartItems.Count == 0)
            {
                TempData["EmptyCartError"] = "Ваша корзина пуста! Добавьте товары в корзину перед оформлением заказа.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Checkout", "Order");
        }
    }
}
