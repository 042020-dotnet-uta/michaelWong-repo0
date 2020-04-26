using System;

namespace CustomerApplication
{
    public class Product
    {
        #region Fields
        public readonly String id;
        private String _name;
        public String name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }
        private int _price;
        public int price
        {
            get
            {
                return _price;
            }
            private set
            {
                _price = value;
            }
        }
        public readonly int locationID;
        public readonly String[] tags;
        #endregion
    }
}