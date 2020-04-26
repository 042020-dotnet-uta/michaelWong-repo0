using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class Location
    {
        #region Fields
        private static Regex rx;
        public readonly String id;
        private String _name;
        public String name
        {
            get
            {
                return _name;
            }
            private  set
            {
                if(rx.IsMatch(value.Trim()))
                _name = value.Trim();
            }
        }
        #endregion

        #region Constructors
        public Location(String _id, String _str)
        {
            id = _id;
            name = _str;
        }
        #endregion

        #region Methods
        #endregion
    }
}