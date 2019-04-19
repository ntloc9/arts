using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.DAL
{
    public class artsDBContext : DbContext
    {

        public artsDBContext() : base("artsDBContext") { }

        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> brands { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Wishlist> wishlists { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Feedback> feedbacks { get; set; }
        public DbSet<OrderProduct> orderProducts { get; set; }
        public DbSet<Shipping> shippings { get; set; }
        public DbSet<AdminEmployee> adminEmployees { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}