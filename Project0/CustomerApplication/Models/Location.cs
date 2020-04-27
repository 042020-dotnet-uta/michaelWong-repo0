using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication
{
    public class Location
    {
        #region Fields
        [Key]
        public long id {get; private set;}
        private String _name;
        public String name
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
        public Location(long _id, String _str)
        {
            id = _id;
            name = _str;
        }
        #endregion

        #region Methods
        #endregion
    }
}