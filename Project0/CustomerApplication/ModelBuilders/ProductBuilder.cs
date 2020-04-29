using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class ProductBuilder
    {
        #region Fields
        private static Regex PriceRx = new Regex(@"^\d+\.\d{2}$");
        private static Regex NameRx = new Regex(@"^[\w\s]+$");
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
        private long LocationID {get; set;}
        #endregion

        #region Constructor
        public ProductBuilder(long locationID)
        {
            LocationID = locationID;
        }
        #endregion

        #region Methods
        public Product BuildProduct()
        {
            Console.WriteLine("Creating a New Product\n");
            try
            {
                NameInput();
                PriceInput();
                using(var db = new CustomerApplicationContext())
                {
                    Product p = db.Products.Add(new Product {Name = Name, Price = Price, Quantity = 0, LocationID = LocationID}).Entity;
                    db.SaveChanges();
                    return p;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
                return null;
            }
        }

        public void NameInput()
        {
            Console.WriteLine("Enter Product Name:");
            Name = Console.ReadLine();
        }

        public void PriceInput()
        {
            Console.WriteLine("Enter Product Price:");
            Price = Console.ReadLine();
        }
        #endregion
    }
}