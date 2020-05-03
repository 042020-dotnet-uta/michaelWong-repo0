using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Models
{
    public class Location
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public String Name { get; set; }
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