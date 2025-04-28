using Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = Guid.Parse("E0DCFA80-9153-4688-911F-962B8481C4F1"),
                        Name = "Product 1",
                        Description = "Description test product 1",
                        Price = 20,
                        Quantity = 100
                    },
                    new Product
                    {
                        Id = Guid.Parse("1AB8CC12-79AC-404D-AAE8-D6A2886FAF12"),
                        Name = "Product 2",
                        Description = "Description test product 2",
                        Price = 30,
                        Quantity = 200
                    },
                    new Product
                    {
                        Id = Guid.Parse("5C1525D6-BC65-4A5B-98D3-4A08BAAD5ED2"),
                        Name = "Product 3",
                        Description = "Description test product 3",
                        Price = 40,
                        Quantity = 300
                    }
                );
        }
    }
}
