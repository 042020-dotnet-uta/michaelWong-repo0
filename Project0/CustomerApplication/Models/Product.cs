using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Product
    {
        #region Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID{get; set;}
        public String Name{get; set;}
        public String Price{get; set;}
        public int Quantity{get; set;}
        [ForeignKey("Location")]
        public long LocationID{get; set;}
        //public Location Location {get;}
        //public ICollection<Order> Orders{get;}
        //public readonly String[] tags;
        #endregion

        #region Constructors
        public Product(){}
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{ID}:\t{Name} ({Quantity}, ${Price})";
        }
        #endregion
    }
}