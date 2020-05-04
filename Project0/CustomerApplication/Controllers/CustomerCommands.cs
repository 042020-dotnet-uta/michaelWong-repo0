using System;
using System.Collections.Generic;
using CustomerApplication.Models;
using CustomerApplication.DbAccess;

/// <summary>
/// The <c>CustomerCommands</c> class.
/// Implements the <c>ICommands</c> interface.
/// Contains all business logic for commands issued by a Customer user.
/// </summary>
namespace CustomerApplication.Controllers
{
    public class CustomerCommands : ICommands
    {
        #region Fields
        public UserTerminal UI;
        public List<Action> CommandsMain;
        public List<Action> CommandsLocation;
        #endregion

        #region Constructors
        public CustomerCommands(UserTerminal ui)
        {
            UI = ui;
            CommandsMain = new List<Action>();
            CommandsLocation = new List<Action>();
            GetCommandsMain();
            GetCommandsLocation();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds methods to <c>CommandsMain</c> which the user will have access to in the main menu.
        /// </summary>
        public void GetCommandsMain()
        {
            CommandsMain.Add(LogOut);
            CommandsMain.Add(ViewProfile);
            CommandsMain.Add(ViewOrderHistory);
            CommandsMain.Add(ViewLocations);
        }

        /// <summary>
        /// Adds methods to <c>CommandsLocation</c> which the user will have access to in the location menu.
        /// </summary>
        public void GetCommandsLocation()
        {
            CommandsLocation.Add(GoBack);
            CommandsLocation.Add(ViewInvetory);
            CommandsLocation.Add(PlaceOrders);
        }

        /// <summary>
        /// Displays commands from <c>CommandsMain</c> or <CommandsLocation</c>.
        /// Used in the the <c>UserTerminal</c> Run method loop.
        /// Depends on the state of the Location field.
        /// null => display main menu.
        /// <c>Location</c> instance => display location menu.
        /// </summary>
        public void DisplayCommands()
        {
            //Location is null when user is in the main menu.
            if (UI.Location == null)
            {
                Console.WriteLine("Main Menu:\n");
                for (int i = 0; i < CommandsMain.Count; i++)
                {
                    Console.WriteLine($"{i}:\t{CommandsMain[i].Method.Name}");
                }
                Console.Write("\n> ");
                try
                {
                    //Console command input.
                    int input = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    CommandsMain[input]();
                }
                catch (System.Exception)
                {
                    CommandError();
                }
            }

            //Location is not null. Display the location menu instead.
            else
            {
                CurrentLocation();
                for (int i = 0; i < CommandsLocation.Count; i++)
                {
                    Console.WriteLine($"{i}:\t{CommandsLocation[i].Method.Name}");
                }
                Console.Write("\n> ");
                try
                {
                    //Console command input.
                    int input = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    CommandsLocation[input]();
                }
                catch (System.Exception)
                {
                    CommandError();
                }
            }
        }

        #region Main Menu
        /// <summary>
        /// Method for terminating the <c>UserTerminal</c> Run method loop.
        /// Sets <c>User</c> field to null.
        /// Terminates the process.
        /// </summary>
        public void LogOut()
        {
            Console.WriteLine("Logging out...\n");
            UI.User = null;
            Continue();
        }

        /// <summary>
        /// Displays information of the current <c>User</c>.
        /// </summary>
        public void ViewProfile()
        {
            Console.WriteLine("Viewing Profile:\n\n" + UI.User + "\n");
            Continue();
        }

        /// <summary>
        /// Displays all <c>Order</c> in the database which references the current <c>User</c>.
        /// </summary>
        public void ViewOrderHistory()
        {
            try
            {
                ICollection<Order> orderHistory = new OrderDb().GetUserHistory(UI.User.Id);
                Console.WriteLine("Showing Order History\n");
                foreach (var order in orderHistory)
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

        /// <summary>
        /// Display all <c>Location</c> in the database.
        /// Accepts console input to view a <c>Location</c>.
        /// Sets appropriate field in <c>UserTerminal</c>.
        /// </summary>
        public void ViewLocations()
        {
            try
            {
                //Loads locations from database.
                ICollection<Location> locations = new LocationDb().GetLocations();
                Console.WriteLine("Choose a Location to View:\n");
                Console.WriteLine("0:\tReturn");
                foreach (Location location in locations)
                {
                    Console.WriteLine(location);
                }
                Console.Write("\nEnter Location Id:\n> ");

                //Console input.
                var input = Int32.Parse(Console.ReadLine());
                Console.Clear();

                //0: Return
                if (input == 0)
                {
                    GoBack();
                    return;
                }
                //Load location from database.
                UI.Location = new LocationDb().GetLocation(input);
            }
            catch (System.Exception)
            {
                CommandError();
            }
        }
        #endregion

        #region Location Menu
        /// <summary>
        /// Sets <c>Location</c> field of <c>UserTerminal</c> to null.
        /// Returns to main menu.
        /// </summary>
        public void GoBack()
        {
            Console.WriteLine("Going back to main menu...\n");
            UI.Location = null;
            Continue();
        }

        /// <summary>
        /// Display all <c>Product</c> with reference to the current <c>Location</c> in the database.
        /// </summary>
        public void ViewInvetory()
        {
            CurrentLocation();
            Console.WriteLine("Showing Inventory:\n");
            DisplayInventory();
            Console.WriteLine();
            Continue();
        }

        /// <summary>
        /// Accepts console input.
        /// Instantiates new <c>Order</c> instances which reference <c>Product</c> in the current inventory.
        /// Decrements product quantity in database.
        /// </summary>
        public void PlaceOrders()
        {
            try
            {
                CurrentLocation();
                Console.WriteLine("0:\tReturn");
                DisplayInventory();
                Console.Write("\nExample: 42 12, 13 2, 889 3\nEnter ID Quantity of Products Separated By Commas:\n> ");

                //Console input.
                var input = Console.ReadLine();
                Console.Clear();

                //0: Return
                if (input == "0")
                {
                    Continue();
                    return;
                }
                //Creates new order instances and inserts into database.
                var ordersPlaced = new OrderDb().PlaceOrders(input, UI.User.Id, UI.Location.Id);

                //Displays Order details.
                Console.WriteLine("Orders placed.\n");
                double total = 0;
                foreach (var order in ordersPlaced)
                {
                    total += Double.Parse(order.Product.Price) * order.Quantity;
                    Console.WriteLine(order);
                }
                Console.WriteLine($"Total Price:\t${String.Format("{0:0.00}", total)}\n");
                Continue();
            }
            catch (System.Exception)
            {
                CommandError();
            }
        }

        /// <summary>
        /// Displays all <c>Product</c> which reference the current <c>Location</c> in the database.
        /// </summary>
        public void DisplayInventory()
        {
            ICollection<Product> products = new ProductDb().GetProducts(UI.Location.Id);
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
        #endregion

        /// <summary>
        /// Generates message for invalid command input.
        /// </summary>
        public void CommandError()
        {
            Console.Clear();
            Console.WriteLine("An error occured.");
            Continue();
        }

        /// <summary>
        /// Generates message for current <c>Location</c>.
        /// </summary>
        public void CurrentLocation()
        {
            Console.WriteLine($"Current Location:\n{UI.Location}\n");
        }

        /// <summary>
        /// Generates message and waits for console input.
        /// </summary>
        public void Continue()
        {
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }
        #endregion
    }
}