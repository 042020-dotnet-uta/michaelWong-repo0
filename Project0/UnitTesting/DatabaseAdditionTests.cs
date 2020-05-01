using System.Linq;
using Xunit;
using CustomerApplication;
using Microsoft.EntityFrameworkCore;

namespace UnitTesting
{
    public partial class CustomerApplicationTests
    {
        [Fact]
        public void AddUserTypeToDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AddUserTypeToDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType {Description = "Administrator user. Access to tools to create new users, locations and products. Able to manage inventories.", Name = "Admin"});
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Single(db.UserTypes);
            }
        }

        [Fact]
        public void AddUserToDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AddUserToDatabase")
                .Options;
            
            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType {Description = "Something", Name = "Something"});
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                UserType admin = db.UserTypes.Find(1);
                db.Users.Add(new User("Something", "Something", "Something", admin));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Single(db.Users);
            }
        }

        [Fact]
        public void AddLocationToDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AddLocationToDatabase")
                .Options;
            
            using(var db = new CustomerApplicationContext(options))
            {
                db.Locations.Add(new Location("Some Place"));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Single(db.Locations);
            }  
        }

        [Fact]
        public void AddProductToDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AddProductToDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.Locations.Add(new Location("Some Place"));
                db.SaveChanges();
                db.Products.Add(new Product("Some Product", "1.00", db.Locations.Find(1)));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Single(db.Products);
            }
        }

        [Fact]
        public void AddOrderToDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "AddOrderToDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.Find(1)));
                db.Products.Add(new Product("Something", "1.00", db.Locations.Find(1)));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.Find(1), db.Products.Find(1), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Single(db.Orders);
            }
        }
    }
}