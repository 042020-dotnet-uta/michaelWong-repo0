using System;

namespace CustomerApplication
{
    public class Product
    {
        #region Fields
        public readonly String id;
        private String _name;
        public String name
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
        public String price
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
        public readonly String locationID;
        //public readonly String[] tags;
        private int _quantity;
        public int quantity
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
        public Product(String productID, String _locationID, String productName, String productPrice, int productQuantity)
        {
            id = productID;
            locationID = _locationID;
            name = productName;
            price = productPrice;
            quantity = productQuantity;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{id}: {name} - ${price}";
        }
        #endregion
    }
}