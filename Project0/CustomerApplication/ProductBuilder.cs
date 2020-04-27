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
        private long LocationID{get;}
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
            Console.WriteLine("Creating a new product:\n");
            NameInput();
            PriceInput();
            return new Product(923748902734, LocationID, Name, Price, 0); 
        }

        public String NameInput()
        {
            bool check = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter Product Name:");
                    Name = Console.ReadLine();
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            } while (check);
            Console.WriteLine();
            return Name;
        }

        public String PriceInput()
        {
            bool check = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter Product Price:");
                    Price = Console.ReadLine();
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n");
                }
            } while (check);
            Console.WriteLine();
            return Price;
        }
        #endregion
    }
}