using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication
{
    public class Location
    {
        #region Fields
        [Key]
        public long ID {get; private set;}
        private String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            private  set
            {
                _name = value;
            }
        }
        //public ICollection<Product> Products{get;}
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
        #endregion
    }
}