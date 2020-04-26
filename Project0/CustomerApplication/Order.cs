using System;

namespace CustomerApplication
{
    public struct Order
    {
        #region Fields
        public String customerID;
        public String locationID;
        public String productID;
        public int quantity;
        public DateTime timestamp;
        #endregion

        #region Constructors
        public Order(String _customerID, String _locationID, String _productID, int _quantity, DateTime _timestamp)
        {
            customerID = _customerID;
            locationID = _locationID;
            productID = _productID;
            quantity = _quantity;
            timestamp = _timestamp;
        }

        public Order(String _customerID, String _locationID, String _productID, int _quantity)
        {
            customerID = _customerID;
            locationID = _locationID;
            productID = _productID;
            quantity = _quantity;
            timestamp = DateTime.Now;
        }
        #endregion
    }
}