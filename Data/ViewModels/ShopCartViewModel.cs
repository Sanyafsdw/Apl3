using WebApplication3.Data.Models.Cart;
using WebApplication3.Data.Models;

namespace WebApplication3.Data.ViewModels
{
    public class ShopCartViewModel
    {
        public ShopCartItem ShopCartItem { get; set; }
        public ShopCart ShopCart { get; set; }
        public List<Car> Cars { get; set; }


    }
}
