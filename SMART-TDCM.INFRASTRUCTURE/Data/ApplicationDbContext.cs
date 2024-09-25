using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMART_TDCM.CORE.Models;

namespace SMART_TDCM.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>Options):base(Options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            if(Database.ProviderName=="Microsoft.EntityFrameworkCore.SqlServer")
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties=entityType.ClrType.GetProperties().Where(p=>p.PropertyType
                    ==typeof(decimal));

                    foreach(var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                        .HasConversion<double>();
                    }
                }
            }
        }
    }
}