using WebApplication3.Data.Models;

namespace WebApplication3.Data.ViewModels
{
    public class AddCarViewModel
    {
        public Car Car { get; set; }
        public List<Category> Categories { get; set; }
    }
}
