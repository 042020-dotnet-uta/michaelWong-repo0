using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
        public void GetCommandsMain()
        {
            CommandsMain.Add(LogOut);
            CommandsMain.Add(ViewProfile);
            CommandsMain.Add(ViewOrderHistory);
            CommandsMain.Add(ViewLocations);
        }

        public void GetCommandsLocation()
        {
            CommandsLocation.Add(GoBack);
            CommandsLocation.Add(ViewInvetory);
            CommandsLocation.Add(PlaceOrders);
        }

        public void DisplayCommands()
        {
            if (UI.Location == null)
            {
                for (int i = 0; i < CommandsMain.Count; i++)
                {
                    Console.WriteLine($"{i}:\t{CommandsMain[i].Method.Name}");
                }
                Console.Write("\n> ");
                try
                {
                    int input = Int32.Parse(Console.ReadLine());
                    //Console.Clear();
                    CommandsMain[input]();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    //CommandError();
                }
            }
            else
            {
                Console.WriteLine($"Current Location: {UI.Location}\n");
                for (int i = 0; i < CommandsLocation.Count; i++)
                {
                    Console.WriteLine($"{i}:\t{CommandsLocation[i].Method.Name}");
                }
                Console.Write("\n> ");
                try
                {
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
        public void LogOut()
        {
            Console.WriteLine("Logging out...\n");
            UI.User = null;
            Continue();
        }

        public void ViewProfile()
        {
            Console.WriteLine(UI.User + "\n");
            Continue();
        }

        public void ViewOrderHistory()
        {
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    Console.WriteLine("Showing Order History:\n");
                    var user = db.Users.Find(UI.User.Id);
                    var orders = db.Orders
                        .Where(order => order.User.Id == user.Id)
                        .Include(order => order.Product)
                        .ThenInclude(product => product.Location)
                        .ToList();
                    Console.WriteLine(orders.Count);
                    foreach (var order in orders)
                    {
                        Console.WriteLine("In loop");
                        Console.WriteLine(order);
                    }
                    Console.WriteLine();
                    Continue();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    //CommandError();
                }
            }
        }

        public void ViewLocations()
        {
            Console.WriteLine("Choose a Location to View:\n");
            Console.WriteLine("0:\tReturn");
            using (var db = new CustomerApplicationContext())
            {
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
                    var input = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    if (input == 0)
                    {
                        GoBack();
                        return;
                    }
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
        public void GoBack()
        {
            Console.WriteLine("Going back to main menu...\n");
            UI.Location = null;
            Continue();
        }

        public void ViewInvetory()
        {
            CurrentLocation();
            DisplayInventory();
            Continue();
        }

        public void PlaceOrders()
        {
            CurrentLocation();
            Console.WriteLine("\n0:\tReturn");
            DisplayInventory();
            Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter ID Quantity of Products Separated By Commas:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    var input = Console.ReadLine();
                    Console.Clear();
                    if (input == "0")
                    {
                        GoBack();
                        return;
                    }
                    var user = db.Users.Find(UI.User.Id);
                    var location = db.Locations.Find(UI.Location.Id);
                    foreach (var splitElement in input.Split(","))
                    {
                        var quants = splitElement.Trim().Split(" ");
                        var id = Int32.Parse(quants[0]);
                        var quant = Int32.Parse(quants[1]);
                        var product = db.Products.Find(id);
                        if (product.Location.Id != location.Id || (product.Quantity - quant) < 0) throw new Exception("This threw.");
                        product.Quantity -= quant;
                        db.Orders.Add(new Order(user, product, quant));
                    }
                    db.SaveChanges();
                    Console.WriteLine("Orders placed.");
                    Continue();
                }
                catch
                {
                    CommandError();
                }
            }
        }

        public void DisplayInventory()
        {
            using (var db = new CustomerApplicationContext())
            {
                Console.WriteLine("Id:\tName (Quantity, Price)");
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

        public void CommandError()
        {
            Console.Clear();
            Console.WriteLine("Invalid command.");
            Continue();
        }

        public void CurrentLocation()
        {
            Console.WriteLine($"Current Location: {UI.Location}\n");
        }

        public void Continue()
        {
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }
        #endregion
    }
}