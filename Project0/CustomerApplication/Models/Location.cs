using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Location
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
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Constructors
        private Location() { } //For Entity Framework
        public Location(String name)
        {
            Name = name;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{Id}:\t{Name}";
        }
        #endregion
    }
}