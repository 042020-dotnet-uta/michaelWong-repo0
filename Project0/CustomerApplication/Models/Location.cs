using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class Location
    {
        #region Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID {get; set;}
        public String Name{get; set;}
        #endregion

        #region Constructors
        public Location(){}
        public Location(long id, String name)
        {
            ID = id;
            Name = name;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"{ID}: {Name}";
        }
        #endregion
    }
}