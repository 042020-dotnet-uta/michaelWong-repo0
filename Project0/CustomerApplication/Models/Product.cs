using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Product
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
        private String _name;
        [Required]
        [MaxLength(50)]
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private String _price;
        [Required]
        public String Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
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
        [Required]
        public Location Location { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion

        #region Constructors
        private Product() { } //For Entity Framework
        public Product(String name, String price, Location location)
        {
            Name = name;
            Price = price;
            Quantity = 0;
            Location = location;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{Id}:\t{Name} ({Quantity}, ${Price})";
        }
        #endregion
    }
}