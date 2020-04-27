using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Product
    {
        #region Fields
        [Key]
        public long ID{get;}
        private String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }
        private String _price;
        public String Price
        {
            get
            {
                return _price;
            }
            private set
            {
                _price = value;
            }
        }
        [ForeignKey("Location")]
        public long LocationID{get;}
        //public readonly String[] tags;
        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        #endregion

        #region Constructors
        public Product(){}
        public Product(long productID, long locationID, String productName, String productPrice, int productQuantity)
        {
            ID = productID;
            LocationID = locationID;
            Name = productName;
            Price = productPrice;
            Quantity = productQuantity;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{ID}: {Name} - ${Price}";
        }
        #endregion
    }
}