using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

namespace CustomerApplication.DbAccess
{
    public class ProductDb
    {
        #region Fields
        private static Regex PriceRx = new Regex(@"^\d+\.\d{2}$");
        private static Regex NameRx = new Regex(@"^[\w\s]{1,50}$");
        private String _name;
        private String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (NameRx.IsMatch(value.Trim())) _name = value.Trim();
                else throw new FormatException("Invalid product name input.");
            }
        }
        private String _price;
        private String Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (PriceRx.IsMatch(value)) _price = value;
                else throw new FormatException("Invalid product price input. Format: X.XX");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validates console input to instantiate a new product and insert it into the database.
        /// </summary>
        /// <param name="locationId">Id of location instance which the new product will reference.</param>
        /// <returns>
        /// null if console input fails validation.
        /// Product object if new location inserted into database.
        /// </returns>
        public Product Build(int locationId)
        {

            using (var db = new CustomerApplicationContext())
            {
                Console.WriteLine("Creating a New Product\n");
                NameInput();
                PriceInput();
                var location = db.Locations.Find(locationId);
                var product = db.Products.Add(new Product(Name, Price, location)).Entity;
                db.SaveChanges();
                return product;
            }

        }

        /// <summary>
        /// Gets console input for product name.
        /// </summary>
        public void NameInput()
        {
            Console.Write("Enter Product Name:\n> ");
            Name = Console.ReadLine();
        }

        /// <summary>
        /// Gets console input for product price.
        /// Format: X.XX.
        /// </summary>
        /// <example>
        /// 1.00 15.50 190.99
        /// </example>
        public void PriceInput()
        {
            Console.Write("Enter Product Price:\n> ");
            Price = Console.ReadLine();
        }

        public Product GetProduct(int id)
        {
            using (var db = new CustomerApplicationContext())
            {
                return db.Products.Find(id);
            }
        }

        public ICollection<Product> GetProducts(int locationId)
        {
            using (var db = new CustomerApplicationContext())
            {
                db.Locations.Find(locationId);
                return db.Products
                    .AsNoTracking()
                    .Where(product => product.Location.Id == locationId)
                    .Include(product => product.Location)
                    .ToList();
            }
        }
        #endregion

        public Product RemoveProduct(int productId, int locationId)
        {
            using (var db = new CustomerApplicationContext())
            {
                //Loads current location from database.
                var location = db.Locations.Find(locationId);

                //Load product which match the input id.
                var product = db.Products.Find(productId);

                //Checks the product references the current location.
                if (product.Location.Id != locationId) throw new Exception();

                //Deletes product from current location.
                db.Products.Remove(product);
                db.SaveChanges();
                return product;
            }

        }

        public ICollection<Product> UpdateInventory(string input, int locationId)
        {

            using (var db = new CustomerApplicationContext())
            {
                //Loads current location from database.
                var location = db.Locations.Find(locationId);
                List<Product> productsUpdated = new List<Product>();
                foreach (var productsElement in input.Split(","))
                {
                    //Parses input.
                    var productQuery = productsElement.Trim().Split(" ");
                    var productId = Int32.Parse(productQuery[0]);
                    var quantity = Int32.Parse(productQuery[1]);
                    var product = db.Products.Find(productId);

                    //Verifies all products reference the current location.
                    if (product.Location.Id != locationId || (product.Quantity + quantity) < 0) throw new Exception();
                    product.Quantity += quantity;
                    productsUpdated.Add(product);
                }
                db.SaveChanges();
                return productsUpdated;
            }

        }
    }
}