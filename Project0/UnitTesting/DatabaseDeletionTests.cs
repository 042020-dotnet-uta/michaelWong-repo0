using System.Linq;
using Xunit;
using CustomerApplication;
using Microsoft.EntityFrameworkCore;

namespace UnitTesting
{
    public partial class CustomerApplicationTests
    {
        [Fact]
        public void DeleteUsertypeFromDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "DeleteUsertypeFromDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.First()));
                db.Products.Add(new Product("Something", "1.00", db.Locations.First()));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.First(), db.Products.First(), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Remove(db.UserTypes.Include(userType => userType.Users).ThenInclude(user => user.Orders).First());
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Empty(db.UserTypes);
                Assert.Empty(db.Users);
                Assert.Empty(db.Orders);
            }
        }

        [Fact]
        public void RemoveUserFromDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "RemoveUserFromDatabase")
                .Options;
            
            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.First()));
                db.Products.Add(new Product("Something", "1.00", db.Locations.First()));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.First(), db.Products.First(), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                db.Users.Remove(db.Users.Include(user => user.Orders).First());
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Empty(db.Users);
                Assert.Empty(db.Orders);
            }
        }
        
        [Fact]
        public void RemoveLocationFromDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "RemoveLocationFromDatabase")
                .Options;
            
            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.First()));
                db.Products.Add(new Product("Something", "1.00", db.Locations.First()));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.First(), db.Products.First(), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                db.Locations.Remove(db.Locations.Include(location => location.Products).ThenInclude(product => product.Orders).First());
                db.SaveChanges();
            } 

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Empty(db.Locations);
                Assert.Empty(db.Products);
                Assert.Empty(db.Orders);
            }
        }

        [Fact]
        public void RemoveProductFromDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "RemoveProductFromDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.First()));
                db.Products.Add(new Product("Something", "1.00", db.Locations.First()));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.First(), db.Products.First(), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                db.Products.Remove(db.Products.Include(product => product.Orders).First());
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Empty(db.Products);
                Assert.Empty(db.Orders);
            }
        }

        [Fact]
        public void RemoveOrderFromDatabase()
        {
            var options = new DbContextOptionsBuilder<CustomerApplicationContext>()
                .UseInMemoryDatabase(databaseName: "RemoveOrderFromDatabase")
                .Options;

            using(var db = new CustomerApplicationContext(options))
            {
                db.UserTypes.Add(new UserType() {Description = "Something", Name = "Something"});
                db.Locations.Add(new Location("Something"));
                db.SaveChanges();
                db.Users.Add(new User("Something", "Something", "Something", db.UserTypes.First()));
                db.Products.Add(new Product("Something", "1.00", db.Locations.First()));
                db.SaveChanges();
                db.Orders.Add(new Order(db.Users.First(), db.Products.First(), 10));
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                db.Orders.Remove(db.Orders.First());
                db.SaveChanges();
            }

            using(var db = new CustomerApplicationContext(options))
            {
                Assert.Empty(db.Orders);
            }
        }
    }
}