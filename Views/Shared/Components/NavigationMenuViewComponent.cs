using WebApplication3.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.DbContext;

namespace net8.Views.Shared.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public StoreContext db;
        public NavigationMenuViewComponent(StoreContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["catId"];
            return View(db.CategoryTable.ToList());
        }
    }
}
