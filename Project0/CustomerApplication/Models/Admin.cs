using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication
{
    public class Admin: User
    {
        #region Fields
        #endregion

        #region Constructors
        public Admin() : base(){}
        public Admin(String first, String last, long id) : base(first, last, id){}
        #endregion
        
        #region Methods
        public override String ToString()
        {
            return "User Type:\tAdmin\n"
                + base.ToString();
        }
        #endregion
    }
}