using System;
using CustomerApplication.Models;
using CustomerApplication.DbAccess;


/// <summary>
/// The <c>AdminCommands</c> class.
/// Implements the <c>ICommands</c> interface.
/// Contains all business logic for commands issued by an Admin user.
/// </summary>
namespace CustomerApplication.Controllers
{
    public class AdminCommands : CustomerCommands
    {
        #region Constructors
        public AdminCommands(UserTerminal ui) : base(ui)
        {
            GetCommandsMain();
            GetCommandsLocation();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds methods to <c>CommandsMain</c> which the user will have access to in the main menu.
        /// Adds the admin commands to the <c>CommandsMain</c>.
        /// </summary>
        public new void GetCommandsMain()
        {
            CommandsMain.Add(SearchCustomer);
            CommandsMain.Add(AddAdmin);
            CommandsMain.Add(AddCustomer);
            CommandsMain.Add(AddLocation);
            CommandsMain.Add(RemoveLocation);
        }

        /// <summary>
        /// Adds methods to <c>CommandsLocation</c> which the user will have access to in the location menu.
        /// Adds the admin commands to the <c>CommandsLocation</c>.
        /// </summary>
        public new void GetCommandsLocation()
        {
            CommandsLocation.Add(AddProduct);
            CommandsLocation.Add(RemoveProduct);
            CommandsLocation.Add(ManageInventory);
            CommandsLocation.Add(ShowOrderHistory);
        }

        #region Main Menu
        /// <summary>
        /// Searches for users by console input.
        /// Search by first name and last name.
        /// Displays user details and order history.
        /// </summary>
        public void SearchCustomer()
        {
            try
            {
                //Console input.
                Console.Write("Enter User First Name:\n> ");
                string firstName = Console.ReadLine();
                Console.Write("Enter User Last Name:\n> ");
                string lastName = Console.ReadLine();
                Console.Clear();

                //Searches for users with inputted first name and last name.
                var users = new UserDb().SearchByName(firstName, lastName);
                Console.WriteLine("Displaying Found Users:\n");
                foreach (var user in users)
                {
                    //Displays user details.
                    Console.WriteLine(user);

                    //Loads orders references the user and displays.
                    var orders = new OrderDb().GetUserHistory(user.Id);
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine();
                }
                Continue();
            }
            catch (System.Exception)
            {
                CommandError();
            }
        }

        /// <summary>
        /// Creates a new <c>User</c> and inserts into the database.
        /// Method is called from <c>AddAdmin</c> and <c>AddCustomer</c>.
        /// </summary>
        /// <param name="userTypeId">An int which corresponds to the Id of UserType in the database.</param>
        public void AddUser(int userTypeId)
        {
            try
            {
                //New instance of UserDb. UserTypeId determines the UserType of the new user.
                User user = new UserDb().Build(userTypeId);
                Console.Clear();
                Console.WriteLine("New user added.\n");
                Console.WriteLine(user + "\n");
                Continue();

            }
            catch (System.Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Continue();
            }
        }

        /// <summary>
        /// <c>AddUser</c> method call with Id=1 for a new Admin.
        /// </summary>
        public void AddAdmin()
        {
            AddUser(1);
        }

        /// <summary>
        /// <c>AddUser</c> method call with Id=2 for a new Customer.
        /// </summary>
        public void AddCustomer()
        {
            AddUser(2);
        }

        /// <summary>
        /// Creates a new <c>Location</c> and inserts into the database.
        /// </summary>
        public void AddLocation()
        {
            try
            {
                //New instance of LocationDb. Returns instance of Location.
                var location = new LocationDb().Build();
                Console.Clear();
                Console.WriteLine("New location added.\n");
                Console.WriteLine(location + "\n");
                Continue();
            }
            catch (System.Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Continue();
            }
        }

        /// <summary>
        /// Displays all <c>Location</c> in the database. Accepts console input to delete a location from the database.
        /// </summary>
        public void RemoveLocation()
        {
            Console.WriteLine("Remove a Location (WARNING: Will remove products and order history):\n");
            Console.WriteLine("0:\tReturn");
            try
            {
                //Loads locations from database.
                var locations = new LocationDb().GetLocations();
                foreach (var location in locations)
                {
                    Console.WriteLine(location);
                }

                //Console input.
                Console.Write("\nEnter Location ID:\n> ");
                var input = Int32.Parse(Console.ReadLine());
                Console.Clear();

                //0: Return
                if (input == 0)
                {
                    Continue();
                    return;
                }

                new LocationDb().RemoveLocation(input);
                Console.WriteLine("Location and products removed.");
                Continue();
            }
            catch (System.Exception)
            {
                CommandError();
            }

        }
        #endregion

        #region Location Menu
        /// <summary>
        /// Create a new <c>Product</c> and inserts into the database.
        /// </summary>
        public void AddProduct()
        {
            CurrentLocation();
            try
            {
                //New instance of ProductDb. Returns a Product instance which references the current location.
                Product product = new ProductDb().Build(UI.Location.Id);
                Console.Clear();
                Console.WriteLine("New product added.\n");
                Console.WriteLine(product + "\n");

                Continue();
            }
            catch (System.Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Continue();
            }
        }

        /// <summary>
        /// Displays all products in the location. Accepts console input to delete a product from the database.
        /// </summary>
        public void RemoveProduct()
        {
            try
            {
                CurrentLocation();
                Console.WriteLine("Remove a Product (WARNING: Will remove order history):\n");
                Console.WriteLine("0:\tReturn");
                DisplayInventory();

                //Console input.
                Console.Write("\nEnter Product ID:\n> ");

                //Removes product from database.
                new ProductDb().RemoveProduct(Int32.Parse(Console.ReadLine()), UI.Location.Id);
                Console.Clear();
                Console.WriteLine("Product removed.");
                Continue();
            }
            catch (SystemException)
            {
                CommandError();
            }
        }

        /// <summary>
        /// Displays all products in the location's inventory. Accepts console input to manage product quantities.
        /// Product quantities can be increased or decreased according to input.
        /// </summary>
        /// <example>
        /// Example input:
        /// [Id] [Quantity], [Id] [Quantity], . . .
        /// 15 12, 301 -20, 400 30
        /// Increase quantity of product 15 by 12. Decrease quantity of product 301 by 20. Increase quantity of product 400 by 30.
        /// </example>
        public void ManageInventory()
        {
            try
            {
                CurrentLocation();
                Console.WriteLine("0:\tReturn");
                DisplayInventory();

                //Console input.
                Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter [Id] [Quantity] of Products Separated By Commas:\n> ");
                var input = Console.ReadLine();
                Console.Clear();

                //0 : Return;
                if (input == "0")
                {
                    Continue();
                    return;
                }

                //Updates product quantities.
                var productsUpdated = new ProductDb().UpdateInventory(input, UI.Location.Id);
                Console.WriteLine("Inventory Updated.\n");
                foreach (var product in productsUpdated)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine();
                Continue();
            }
            catch (System.Exception)
            {
                CommandError();
            }
        }
        #endregion

        /// <summary>
        /// Displays all orders in the database corresponding to products in the current location.
        /// </summary>
        public void ShowOrderHistory()
        {
            try
            {
                CurrentLocation();
                Console.WriteLine("Showing Order History:\n");
                
                //Loads orders from database.
                foreach (var order in new OrderDb().GetLocationHistory(UI.Location.Id))
                {
                    Console.WriteLine(order);
                }
                Console.WriteLine();
                Continue();
            }
            catch (System.Exception)
            {
                CommandError();
            }
        }
        #endregion
    }
}