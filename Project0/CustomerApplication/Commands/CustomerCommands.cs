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
                    Console.Clear();
                    CommandsMain[input]();
                }
                catch
                {
                    CommandError();
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
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
            Console.Clear();
        }

        public void ViewProfile()
        {
            Console.WriteLine(UI.User + "\n");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void ViewOrderHistory()
        {
            //TODO
        }

        public void ViewLocations()
        {
            Console.WriteLine("Choose a Location to View:\n");
            Console.WriteLine("0: Return");
            using (var db = new CustomerApplicationContext())
            {
                List<Location> locations = db.Locations
                    .AsNoTracking()
                    .ToList();
                foreach (Location location in locations)
                {
                    Console.WriteLine(location);
                }
                Console.Write("\nEnter Location ID:\n> ");

                try
                {
                    var input = Int64.Parse(Console.ReadLine());
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
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public void ViewInvetory()
        {
            CurrentLocation();
            DisplayInventory();
            Console.WriteLine("\nPress enter to go back.");
            Console.ReadLine();
            Console.Clear();
        }

        public void PlaceOrders()
        {
            CurrentLocation();
            Console.WriteLine("\n0: Return");
            DisplayInventory();
            Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter ID Quantity of Products Separated By Commas:\n> ");
            String input = Console.ReadLine();
            Console.Clear();
            if (input == "0")
            {
                GoBack();
                return;
            }
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    foreach (var splitElement in input.Split(","))
                    {
                        var quants = splitElement.Trim().Split(" ");
                        var id = Int64.Parse(quants[0]);
                        var quant = Int32.Parse(quants[1]);
                        var product = db.Products.Find(id);
                        if (product.LocationID != UI.Location.ID || (product.Quantity - quant) < 0) throw new Exception();
                        product.Quantity -= quant;
                        db.Orders.Add(new Order() { Quantity = quant, Timestamp = DateTime.Now, UserID = UI.User.ID, ProductID = id });
                    }
                    db.SaveChanges();
                    Console.WriteLine("Orders placed. Press enter to continue.");
                    Console.ReadLine();
                    Console.Clear();
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
                Console.WriteLine("ID:\tName (Quantity, Price)");
                List<Product> products = db.Products
                    .AsNoTracking()
                    .Where(p => p.LocationID == UI.Location.ID)
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
            Console.WriteLine("Invalid command. Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public void CurrentLocation()
        {
            Console.WriteLine($"Current Location: {UI.Location}\n");
        }
        #endregion
    }
}