using System;

namespace CustomerApplication
{
    public class Customer: User
    {
        #region Fields
        #endregion

        #region Constructors
        public Customer(String _first, String _last, String _id) : base(_first, _last, _id)
        {

        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return "User Type:\tCustomer\n"
                + base.ToString();
        }
        #endregion
    }
}