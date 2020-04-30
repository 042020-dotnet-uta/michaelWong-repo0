using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The <c>CustomerCommands</c> class.
/// Implements the <c>ICommands</c> interface.
/// Contains all business logic for commands issued by a Customer user.
/// </summary>
namespace CustomerApplication
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
                catch
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
                catch
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
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    Console.WriteLine("Showing Order History:\n");

                    //Load user from database.
                    var user = db.Users.Find(UI.User.Id);

                    //Load orders which reference the user.
                    var orders = db.Orders
                        .Where(order => order.User.Id == user.Id)
                        .Include(order => order.Product)
                        .ThenInclude(product => product.Location)
                        .ToList();
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine();
                    Continue();
                }
                catch
                {
                    CommandError();
                }
            }
        }

        /// <summary>
        /// Display all <c>Location</c> in the database.
        /// Accepts console input to view a <c>Location</c>.
        /// Sets appropriate field in <c>UserTerminal</c>.
        /// </summary>
        public void ViewLocations()
        {
            Console.WriteLine("Choose a Location to View:\n");
            Console.WriteLine("0:\tReturn");
            using (var db = new CustomerApplicationContext())
            {
                //Loads Locations from database.
                List<Location> locations = db.Locations
                    .AsNoTracking()
                    .ToList();
                foreach (Location location in locations)
                {
                    Console.WriteLine(location);
                }
                Console.Write("\nEnter Location Id:\n> ");
                try
                {
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
                    UI.Location = db.Locations.Find(input);
                }
                catch
                {
                    CommandError();
                }
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
            Continue();
        }

        /// <summary>
        /// Accepts console input.
        /// Instantiates new <c>Order</c> instances which reference <c>Product</c> in the current inventory.
        /// Decrements product quantity in database.
        /// </summary>
        public void PlaceOrders()
        {
            CurrentLocation();
            Console.WriteLine("0:\tReturn");
            DisplayInventory();
            Console.Write("\nExample: 42 12, 13 2, 889 3\nEnter ID Quantity of Products Separated By Commas:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    //Console input.
                    var input = Console.ReadLine();
                    Console.Clear();
                    
                    //0: Return
                    if (input == "0")
                    {
                        Continue();
                        return;
                    }

                    //Loads current User and Location from database.
                    var user = db.Users.Find(UI.User.Id);
                    var location = db.Locations.Find(UI.Location.Id);

                    //Store Order instances to be displayed after adding to database.
                    List<Order> ordersPlaced = new List<Order>();
                    foreach (var splitElement in input.Split(","))
                    {

                        //Parses console input.
                        var quants = splitElement.Trim().Split(" ");
                        var id = Int32.Parse(quants[0]);
                        var quant = Int32.Parse(quants[1]);
                        var product = db.Products.Find(id);

                        //Checks Product references current Location and sufficient Quantity. Does not allow orders over 50 Quantity.
                        if (product.Location.Id != location.Id || (product.Quantity - quant) < 0 || quant > 50) throw new Exception("This threw.");
                        product.Quantity -= quant;

                        //Creates new Product instance and inserts into the database.
                        ordersPlaced.Add(db.Orders.Add(new Order(user, product, quant)).Entity);
                    }
                    db.SaveChanges();

                    //Displays Order details.
                    Console.WriteLine("Orders placed.\n");
                    foreach (var order in ordersPlaced)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine();
                    Continue();
                }
                catch
                {
                    CommandError();
                }
            }
        }

        /// <summary>
        /// Displays all <c>Product</c> which reference the current <c>Location</c> in the database.
        /// </summary>
        public void DisplayInventory()
        {
            using (var db = new CustomerApplicationContext())
            {
                //Load products from database which references the current location.
                List<Product> products = db.Products
                    .AsNoTracking()
                    .Where(product => product.Location.Id == UI.Location.Id)
                    .ToList();
                foreach (Product product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }
        #endregion

        /// <summary>
        /// Generates message for invalid command input.
        /// </summary>
        public void CommandError()
        {
            Console.Clear();
            Console.WriteLine("Invalid command.");
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