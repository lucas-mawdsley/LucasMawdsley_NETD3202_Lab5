using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LucasMawdsley_NETD3202_Lab5.Models;

namespace LucasMawdsley_NETD3202_Lab5.Data
{
    public class ProductContext : DbContext
    {
        // Constructor
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        // Declare DbSet properties
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        // Associate the models with their respective tables
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Product>().ToTable("Product");
            modelbuilder.Entity<Vendor>().ToTable("Vendor");
        }
    }
}
