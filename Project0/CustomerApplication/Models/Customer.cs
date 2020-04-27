using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication
{
    public class Customer: User
    {
        #region Fields
        #endregion

        #region Constructors
        public Customer() : base(){}
        public Customer(String first, String last, long id) : base(first, last, id, "customer"){}
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