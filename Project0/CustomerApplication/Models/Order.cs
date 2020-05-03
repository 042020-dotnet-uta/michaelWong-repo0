using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Models
{
    public class Order
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
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