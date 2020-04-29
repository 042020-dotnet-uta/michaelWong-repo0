using System;

namespace CustomerApplication
{
    public class UserTerminal
    {
        #region Fields
        public User User;
        public ICommands Commands;
        public Location Location; //testing purposes
        #endregion

        #region Constructors
        public UserTerminal() { }
        #endregion

        #region Methods
        public void Run()
        {
            while (User == null)
            {
                DisplayLogin();
            }
            if (User.Type == "admin")
            {
                //Commands = new AdminCommands(this);
                Commands = new AdminCommands(this); //For testing purposes
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
                        CreateNewUser("customer");
                        break;
                    default:
                        throw new FormatException();
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Invalid command. Press enter to continue");
                Console.ReadLine();
                Console.Clear();
            }

        }

        public void PromptLogin()
        {
            User u = new Login().PromptLogin();
            Console.Clear();
            if (u == null)
            {
                Console.WriteLine("Failed to login. Press enter to continue.");
            }
            else
            {
                Console.WriteLine("Login successful. Press enter to continue.\n");
                Console.WriteLine(u);
                User = u;
            }
            Console.ReadLine();
            Console.Clear();
        }
        #endregion

        #region CreationMethods
        public void CreateNewUser(String type)
        {
            User u = new UserBuilder().BuildUser(type);
            if (u == null)
            {
                Console.WriteLine("Failed to create a new user. Press enter to continue.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("New user created. Use id and password to sign in.\n");
                Console.WriteLine(u);
                Console.WriteLine("\nPress enter to continue.");
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void CreateLocation()
        {
            Location l = new LocationBuilder().BuildLocation();
            if (l == null)
            {
                Console.WriteLine("Failed to create a new location. Press enter to continue.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("New location created.\n");
                Console.WriteLine(l);
                Console.WriteLine("\nPress enter to continue");
            }
            Console.ReadLine();
            Console.Clear();

            //TESTING PURPOSES
            Location = l;
        }

        public void CreateProduct()
        {
            Product p = new ProductBuilder(Location.ID).BuildProduct();
            if (p == null)
            {
                Console.WriteLine("Failed to create a new product. Press enter to continue.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("New product created.\n");
                Console.WriteLine(p);
                Console.WriteLine("\nPress enter to continue");
            }
            Console.ReadLine();
            Console.Clear();
        }
        #endregion
        #endregion
    }
}