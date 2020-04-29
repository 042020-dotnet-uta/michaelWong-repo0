using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class ProductBuilder
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

        #region Constructor
        #endregion

        #region Methods
        public Product Build(int locationId)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.InnerException.ToString());
                return null;
            }
        }

        public void NameInput()
        {
            Console.Write("Enter Product Name:\n> ");
            Name = Console.ReadLine();
        }

        public void PriceInput()
        {
            Console.Write("Enter Product Price:\n> ");
            Price = Console.ReadLine();
        }
        #endregion
    }
}