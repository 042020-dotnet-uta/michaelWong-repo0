using System;

namespace CustomerApplication
{
    public class UserTerminal
    {
        #region Fields
        public User User;
        public ICommands Commands;
        public Location Location;
        #endregion

        #region Constructors
        public UserTerminal(){}
        #endregion

        #region Methods
        public void Run()
        {
            while (User == null)
            {
                DisplayLogin();
            }
            if (User.UserType.Name == "Admin")
            {
                Commands = new AdminCommands(this);
            }
            else
            {
                Commands = new CustomerCommands(this);
            }
            while (User != null)
            {
                Commands.DisplayCommands();
            }
        }

        #region Menus
        public void DisplayLogin()
        {
            Console.WriteLine("0: Login\n1: New User");
            try
            {
                Console.Write("\n> ");
                int input = Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch (input)
                {
                    case 0:
                        PromptLogin();
                        break;
                    case 1:
                        CreateNewUser();
                        break;
                    default:
                        throw new FormatException();
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Invalid command. Press enter to continue.");
                Console.ReadLine();
                Console.Clear();
            }

        }

        public void PromptLogin()
        {
            User user = new Login().PromptLogin();
            Console.Clear();
            if (user == null)
            {
                Console.WriteLine("Failed to login. Press enter to continue.");
            }
            else
            {
                Console.WriteLine("Login successful. Press enter to continue.\n");
                Console.WriteLine(user);
                User = user;
            }
            Console.ReadLine();
            Console.Clear();
        }
        #endregion

        #region CreationMethods
        public void CreateNewUser()
        {
            User user = new UserBuilder().Build(2);
            if (user == null)
            {
                Console.WriteLine("Failed to create a new user. Press enter to continue.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("New user created. Use id and password to sign in.\n");
                Console.WriteLine(user);
                Console.WriteLine("\nPress enter to continue.");
            }
            Console.ReadLine();
            Console.Clear();
        }
        #endregion
        #endregion
    }
}