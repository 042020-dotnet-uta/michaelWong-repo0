using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Order
    {
        #region Fields
        [Key]
        public long ID{get;}
        [ForeignKey("Customer")]
        public long CustomerID{get;}
        [ForeignKey("Location")]
        public long LocationID{get;}
        [ForeignKey("Product")]
        public long ProductID{get;}
        public int Quantity{get;set;}
        public DateTime Timestamp{get;}
        #endregion

        #region Constructors
        public Order(){}
        public Order(long id, long customerID, long locationID, long productID, int quantity, DateTime timestamp)
        {
            ID = id;
            CustomerID = customerID;
            LocationID = locationID;
            ProductID = productID;
            Quantity = quantity;
            Timestamp = timestamp;
        }
        public Order(long id, long customerID, long locationID, long productID, int quantity)
        {
            CustomerID = customerID;
            LocationID = locationID;
            ProductID = productID;
            Quantity = quantity;
            Timestamp = DateTime.Now;
        }
        #endregion
    }
}