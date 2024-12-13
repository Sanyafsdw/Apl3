using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.DbContext;

namespace WebApplication3.Data.Models.Cart
{
    public class ShopCart
    {
        private StoreContext db;
        public ShopCart(StoreContext context)
        {
            db = context;
        }

        public string ShopCartId { get; set; } //идентификатор всей корзины
        public List<ShopCartItem> ListShopItems { get; set; } = new List<ShopCartItem>(); //список всех товаров в корзине
        /// <summary>
        /// будет определять новая корзина или нет. Если новая, то определит новий идентификатор
        /// </summary>
        /// <returns></returns>
        public static ShopCart GetCart(IServiceProvider service)
        {
            //Переменная, через которую будем работать с сессиями
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<StoreContext>();
            //вернет нам ID корзины
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId); //привязываем к каждому товару сгенерированный ключ
            return new ShopCart(context)
            {
                ShopCartId = shopCartId
            };
        }

        public void AddToCart(Car car)
        {
            //db.ShopCartTable.Add(new ShopCart(db) { ShopCartId = ShopCartId, ListShopItems = ListShopItems });
            db.ShopCartItemTable.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                Car = car,
                Price = car.Price,
            });
            db.SaveChanges();
        }
        public void RemoveFromCart(int shopCartItemId)
        {
            var itemToRemove = db.ShopCartItemTable.FirstOrDefault(c => c.ShopCartItemId == shopCartItemId && c.ShopCartId == ShopCartId);
            if (itemToRemove != null)
            {
                db.ShopCartItemTable.Remove(itemToRemove);
                db.SaveChanges();

                // Обновляем список в объекте
                ListShopItems.Remove(itemToRemove);
            }
        }


        public List<ShopCartItem> GetShopItems()
        {
            return db.ShopCartItemTable.Where(c => c.ShopCartId == ShopCartId).Include(cr => cr.Car).ToList();
        }
    }
}
