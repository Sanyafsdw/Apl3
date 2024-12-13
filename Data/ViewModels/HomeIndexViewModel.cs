using WebApplication3.Data.Models;

namespace WebApplication3.Data.ViewModels
{
    public class HomeIndexViewModel
    {
        public string PageName { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public int CurrentCategory { get; set; }
    }
}
