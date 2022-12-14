using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities.ConfigurationEntities;
using Shop.Entities.SeedData;
using Microsoft.Extensions.Configuration;

namespace Shop.Entities.Enities
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        public ShopContext()
        {

        }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Account> Accounts { set; get; }
        public virtual DbSet<CategoryProduct> CategoryProducts { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<Comment> Comments { set; get; }
        public virtual DbSet<File> Files { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<OrderDetail> OrderDetails { set; get; }
        public virtual DbSet<Payment> Payments { set; get; }
        public virtual DbSet<Menu> Menus { set; get; }
        public virtual DbSet<Rate> Rates { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configData = new ConfigData();
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new CategoryProductConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new FileConfig());
            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new OrderDetailConfig());
            modelBuilder.ApplyConfiguration(new PaymentConfig());
            modelBuilder.ApplyConfiguration(new RateConfig());
            modelBuilder.ApplyConfiguration(new SlideConfig());
            configData.ConfigDataAccount(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //refer: https://docs.microsoft.com/en-us/ef/core/querying/related-data#lazy-loading
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                // Get connection string from WebMVC in appsettings.js file
                // package: Microsoft.Extensions.Configuration.Json
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ShopDb");
                //refer at https://docs.microsoft.com/en-us/ef/core/querying/related-data#include-on-derived-types
                //pacakge: Microsoft.EntityFrameworkCore.SqlServer
                optionsBuilder.UseSqlServer(connectionString).ConfigureWarnings(warning => warning.Throw());

            }
        }

    }
}
