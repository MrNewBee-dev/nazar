using BookShop.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class NazarDbContext:DbContext
    {
        public NazarDbContext(DbContextOptions options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(local);Database=Nazar1988Product;Trusted_Connection=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Product_CategoryMap());
            modelBuilder.Entity<AccounterModel>().HasNoKey();
            //modelBuilder.Entity<ProductsDiscountModel>().HasNoKey();
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product_Category> Product_Categories{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails{ get; set; }

        public DbSet<AccounterModel> View_User_Order { get; set; }
        public DbSet<ProductsDiscountModel> View_Products_Discounts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        
    }
}
