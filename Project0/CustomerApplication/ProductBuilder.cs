using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class ProductBuilder
    {
        #region Fields
        private static Regex priceRx = new Regex(@"^\d+\.\d{2}$");
        private static Regex nameRx = new Regex(@"^[\w\s]+$");
        private String _name;
        private String name
        {
            get
            {
                return _name;
            }
            set
            {
                if (nameRx.IsMatch(value.Trim())) _name = value.Trim();
                else throw new FormatException("Invalid product name input.");
            }
        }
        private String _price;
        private String price
        {
            get
            {
                return _price;
            }
            set
            {
                if (priceRx.IsMatch(value)) _price = value;
                else throw new FormatException("Invalid product price input. Format: X.XX");
            }
        }
        private String locationID;
        #endregion

        #region Constructor
        public ProductBuilder(String _locationID)
        {
            locationID = _locationID;
        }
        #endregion

        #region Methods
        public Product BuildProduct()
        {
            Console.WriteLine("Creating a new product:\n");
            NameInput();
            PriceInput();
            return new Product("923748902734", locationID, name, price, 0); 
        }

        public String NameInput()
        {
            bool check = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter Product Name:");
                    name = Console.ReadLine();
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            } while (check);
            Console.WriteLine();
            return name;
        }

        public String PriceInput()
        {
            bool check = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter Product Price:");
                    price = Console.ReadLine();
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            } while (check);
            Console.WriteLine();
            return price;
        }
        #endregion
    }
}