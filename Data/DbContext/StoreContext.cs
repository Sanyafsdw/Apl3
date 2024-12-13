using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Models;
using WebApplication3.Data.Models.Cart;

namespace WebApplication3.Data.DbContext
{
    public class StoreContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> CarTable { get; set; }
        public DbSet<Category> CategoryTable { get; set; }
        public DbSet<ShopCartItem> ShopCartItemTable { get; set; }
        public DbSet<Order> OrderTable { get; set; }
        public DbSet<OrderDetail> OrderDetailTable { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
