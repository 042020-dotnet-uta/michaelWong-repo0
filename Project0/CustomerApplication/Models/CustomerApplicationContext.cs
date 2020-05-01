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

        public CustomerApplicationContext(){}
        public CustomerApplicationContext(DbContextOptions<CustomerApplicationContext> options) 
            : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(@"Server=DESKTOP-H1F3F9U\SQLEXPRESS;Database=CustomerApp;Trusted_Connection=True;");
        }

    }
}