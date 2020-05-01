using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication
{
    public class CustomerApplicationContext : DbContext
    {
        public DbSet<UserType> UserTypes{get; set;}
        public DbSet<User> Users{get; set;}
        public DbSet<Location> Locations {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<Order> Orders{get; set;}

        public CustomerApplicationContext() : base(GetOptions()) {}
        public CustomerApplicationContext(DbContextOptions<CustomerApplicationContext> options) 
            : base(options){}
        
        private static DbContextOptions GetOptions()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional:false);
            var configuration = builder.Build();
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), configuration.GetConnectionString("CustomerApplicationConnection")).Options;
        }
    }
}