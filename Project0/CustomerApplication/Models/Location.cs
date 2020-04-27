using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomerApplication
{
    public class Location
    {
        #region Fields
        [Key]
        public long id {get;set;}
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