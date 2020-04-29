using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Order
    {
        #region Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID{get; set;}
        public int Quantity{get; set;}
        public DateTime Timestamp{get; set;}
        [ForeignKey("User")]
        public long UserID{get; set;}
        [ForeignKey("Product")]
        public long ProductID{get; set;}
        #endregion
    }
}