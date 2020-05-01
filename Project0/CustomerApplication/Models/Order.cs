using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Order
    {
        #region Fields
        private int _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private int _quantity;
        [Required]
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
        private DateTime _timestamp;
        [Required]
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
            }
        }
        [Required]
        public User User { get; set; }
        [Required]
        public Product Product { get; set; }
        #endregion

        #region Constructors
        private Order() { } //For Entity Framework
        public Order(User user, Product product, int quantity)
        {
            User = user;
            Product = product;
            Quantity = quantity;
            Timestamp = DateTime.Now;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{Product.Location}\n"
                + $"\t{Product.Id}: {Product.Name} ({Quantity}, {Product.Price})\n"
                + $"\t{Timestamp}";
        }
        #endregion
    }
}