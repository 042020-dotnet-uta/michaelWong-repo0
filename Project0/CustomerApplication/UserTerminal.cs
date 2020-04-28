using System;

namespace CustomerApplication
{
    public class UserTerminal
    {
        #region Fields
        private User user;
        #endregion

        #region Constructors
        public UserTerminal(){}
        #endregion
        
        #region Methods
        public void PromptLogin()
        {
            User u = new Login().PromptLogin();
            if (u == null)
            {
                Console.WriteLine("Failed to login. Press enter to continue.");
            }
            else
            {
                Console.WriteLine("Login successful. Press enter to continue.");
                user = u;
            }
            Console.ReadLine();
            Console.Clear();
        }
        #endregion
    }
}