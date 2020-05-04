using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

/// <summary>
/// Controls database access related to <c>Product</c> objects.
/// Used by classes in the business logic layer.
/// </summary>
namespace CustomerApplication.DbAccess
{
    public class ProductDb
    {
        #region Fields
        //Used for price validation.
        private static Regex PriceRx = new Regex(@"^\d+\.\d{2}$");
        //Used for name validation.
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

        /// <summary>
        /// Loads all <c>Product</c> at <c>Location</c>.
        /// </summary>
        /// <param name="locationId">The id of the location being loaded.</param>
        /// <returns>Collection of products.</returns>
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

        /// <summary>
        /// Finds and removes a <c>Product</c> from the database.
        /// Removes by Id.
        /// </summary>
        /// <param name="productId">Id of the product object to be deleted from the database.</param>
        /// <param name="locationId">Id of the current location, used for validation.</param>
        /// <returns>Returns the Product object removed.</returns>
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

        /// <summary>
        /// Increments and decrements product quantities at the location.
        /// Parses input string for <c>Product</c> <c>Id</c> and the amount that <c>Quantity</c> changes.
        /// </summary>
        /// <param name="input">Input string which will be parsed.</param>
        /// <param name="locationId">Id of current location, used for validation.</param>
        /// <returns>Collection of products updated.</returns>
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
        #endregion
    }
}