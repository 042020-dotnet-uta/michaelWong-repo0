using System;

namespace CustomerApplication
{
    public class Login
    {
        #region Constructors
        #endregion

        #region Methods
        public User PromptLogin()
        {
            Console.WriteLine("Enter User ID:");
            String id = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            String password = Console.ReadLine();
            //Send id and password to the database to see if user login is correct
            //Users[] users = db.query something
            User[] users = new User[] { };
            if (users.Length != 0)
            {
                Console.WriteLine("Log in successful.\n");
                return users[0];
            }
            else
            {
                Console.WriteLine("Log in failed.\n");
                return null;
            }
        }
        #endregion
    }
}