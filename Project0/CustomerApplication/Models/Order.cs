using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Order
    {
        #region Fields
        [Key]
        public long ID{get; private set;}
        public int Quantity{get; private set;}
        public DateTime Timestamp{get; private set;}
        [ForeignKey("User")]
        public long UserID{get; private set;}
        public User User{get;}
        [ForeignKey("Product")]
        public long ProductID{get; private set;}
        public Product Product{get;}
        #endregion

        #region Constructors
        public Order(){}
        public Order(long id, long userID, long productID, int quantity, DateTime timestamp)
        {
            ID = id;
            UserID = userID;
            ProductID = productID;
            Quantity = quantity;
            Timestamp = timestamp;
        }
        public Order(long id, long userID, long productID, int quantity)
        {
            UserID = userID;
            ProductID = productID;
            Quantity = quantity;
            Timestamp = DateTime.Now;
        }
        #endregion
    }
}