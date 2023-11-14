using AwesomePizza.Models.OrderModels.OrderRequests;
using AwesomePizza.Models.OrderModels.Orders;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.DAL
{

    public class MyDbContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connection = new SqliteConnection("DataSource=:memory:");
        //    connection.Open();
        //    optionsBuilder.UseSqlite(connection);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}

        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }
    }
}