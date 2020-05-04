using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

namespace CustomerApplication.DbAccess
{
    public class OrderDb
    {
        public ICollection<Order> PlaceOrders(string input, int userId, int locationId)
        {

            using (var db = new CustomerApplicationContext())
            {
                //Loads current User and Location from database.
                var user = db.Users.Find(userId);
                var location = db.Locations.Find(locationId);

                //Stores Order instances to be displayed inserting into database.
                List<Order> ordersPlaced = new List<Order>();
                foreach (var ordersElement in input.Split(","))
                {
                    //Parses input.
                    var orderQuery = ordersElement.Trim().Split(" ");
                    var productId = Int32.Parse(orderQuery[0]);
                    var orderQuantity = Int32.Parse(orderQuery[1]);
                    var product = db.Products.Find(productId);

                    //Verifies Product references current Location and sufficient inventory.
                    if (product.Location.Id != locationId || product.Quantity - orderQuantity < 0 || orderQuantity > 50 || orderQuantity <= 0) throw new Exception();
                    product.Quantity -= orderQuantity;

                    //Creates new Order instance and inserts into database.
                    ordersPlaced.Add(db.Orders.Add(new Order(user, product, orderQuantity)).Entity);
                }
                db.SaveChanges();
                return ordersPlaced;
            }

        }

        public ICollection<Order> GetUserHistory(int userId)
        {

            using (var db = new CustomerApplicationContext())
            {
                var user = db.Users.Find(userId);
                return db.Orders
                    .AsNoTracking()
                    .Where(order => order.User.Id == userId)
                    .Include(order => order.Product)
                    .ThenInclude(product => product.Location)
                    .ToList();
            }

        }

        public ICollection<Order> GetLocationHistory(int locationId)
        {

            using (var db = new CustomerApplicationContext())
            {
                //lLoads location from database.
                var location = db.Locations.Find(locationId);

                //Loads products from database which reference the location.
                var products = db.Products
                    .AsNoTracking()
                    .Where(product => product.Location.Id == locationId)
                    .ToList();

                //Load orders from database which reference the location's products.
                return db.Orders
                    .AsNoTracking()
                    .Where(order => order.Product.Location.Id == locationId)
                    .Include(order => order.Product)
                    .ThenInclude(product => product.Location)
                    .ToList();
            }

        }
    }
}