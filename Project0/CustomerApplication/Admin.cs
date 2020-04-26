using System;

namespace CustomerApplication
{
    public class Admin: User
    {
        #region Fields
        #endregion

        #region Constructors
        public Admin(String _first, String _last, String _id) : base(_first, _last, _id)
        {

        }
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