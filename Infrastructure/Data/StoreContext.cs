using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Reflection;
namespace Infrastructure.Data
{
    // jpa use unit of work and repository patterns as a further abstraction
    // from dbcontext so that it is easier to test and moq. dncontext does not
    // have an interface wghich makes it diffucult to test and moq.
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions options) : base(options)
        {
            // contructor
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }


}