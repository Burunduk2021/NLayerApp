using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using NLayerApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NLayerApp.DAL.EF
{
    public class MobileContext : DbContext
    {

        public MobileContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=NATALIA-PC;Database=EfTestDb;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Phone>().HasData(
                new Phone[]
                {
                    new Phone { Id=1, Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 },
                    new Phone { Id=2, Name = "iPhone 6", Company = "Apple", Price = 320 },
                    new Phone { Id=3, Name = "LG G4", Company = "lG", Price = 260 },
                    new Phone { Id=4, Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 }
                }
            );
        }
    }
}
