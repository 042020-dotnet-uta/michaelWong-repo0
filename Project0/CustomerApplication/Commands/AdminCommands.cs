using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication
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
        public new void GetCommandsMain()
        {
            CommandsMain.Add(AddAdmin);
            CommandsMain.Add(AddCustomer);
            CommandsMain.Add(AddLocation);
            CommandsMain.Add(RemoveLocation);
        }

        public new void GetCommandsLocation()
        {
            CommandsLocation.Add(AddProduct);
            CommandsLocation.Add(RemoveProduct);
            CommandsLocation.Add(ManageInventory);
            CommandsLocation.Add(ShowOrderHistory);
        }

        #region Main Menu
        public void AddUser(int userTypeId)
        {
            User user = new UserBuilder().Build(userTypeId);
            if (user != null)
            {
                Console.Clear();
                Console.WriteLine("New user added.\n");
                Console.WriteLine(user + "\n");
                Continue();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Failed to create new user.");
                Continue();
            }
        }

        public void AddAdmin()
        {
            AddUser(1);
        }

        public void AddCustomer()
        {
            AddUser(2);
        }

        public void AddLocation()
        {
            Location location = new LocationBuilder().Build();
            if (location != null)
            {
                Console.Clear();
                Console.WriteLine("New location added.\n");
                Console.WriteLine(location + "\n");
            }
            Continue();
        }

        public void RemoveLocation()
        {
            Console.WriteLine("Remove a Location (WARNING: Will remove products and order history):\n");
            Console.WriteLine("0:\tReturn");
            using (var db = new CustomerApplicationContext())
            {
                var locations = db.Locations
                    .AsNoTracking()
                    .ToList();
                foreach (Location location in locations)
                {
                    Console.WriteLine(location);
                }
                Console.Write("\nEnter Location ID:\n> ");
                try
                {
                    var input = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    if (input == 0)
                    {
                        Continue();
                        return;
                    }
                    else
                    {
                        var location = db.Locations.Find(input);
                        /*
                        var products = db.Products
                            .Where(p => p.Location.Id == location.Id)
                            .ToList();
                        */
                        db.Locations.Remove(location);
                        //db.Products.RemoveRange(products);
                        db.SaveChanges();
                        Console.WriteLine("Location and products removed.");
                        Continue();
                    }
                }
                catch
                {
                    CommandError();
                }

            }
        }
        #endregion

        #region Location Menu
        public void AddProduct()
        {
            CurrentLocation();
            Product product = new ProductBuilder().Build(UI.Location.Id);
            if (product != null)
            {
                Console.Clear();
                Console.WriteLine("New product added.\n");
                Console.WriteLine(product + "\n");
            }
            Continue();
        }

        public void RemoveProduct()
        {
            CurrentLocation();
            Console.WriteLine("Remove a Product (WARNING: Will remove order history):\n");
            Console.WriteLine("0:\tReturn");
            DisplayInventory();
            Console.Write("\nEnter Product ID:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    var input = Int32.Parse(Console.ReadLine());
                    if (input == 0)
                    {
                        Continue();
                        return;
                    }
                    Console.Clear();
                    db.Locations.Find(UI.Location.Id);
                    var product = db.Products.Find(input);
                    if (product.Location.Id != UI.Location.Id) throw new Exception();
                    db.Products.Remove(product);
                    db.SaveChanges();
                    Console.WriteLine("Product removed.");
                    Continue();
                }
                catch
                {
                    CommandError();

                }
            }
        }

        public void ManageInventory()
        {
            CurrentLocation();
            Console.WriteLine("0:\tReturn");
            DisplayInventory();
            Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter ID Quantity of Products Separated By Commas:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    var input = Console.ReadLine();
                    if (input == "0")
                    {
                        Continue();
                        return;
                    }
                    Console.Clear();
                    var splitInput = input.Split(",");
                    foreach (var splitElement in splitInput)
                    {
                        var quants = splitElement.Trim().Split(" ");
                        var id = Int32.Parse(quants[0]);
                        var quant = Int32.Parse(quants[1]);
                        var location = db.Locations.Find(UI.Location.Id);
                        var product = db.Products.Find(id);
                        if (product.Location.Id != location.Id || (product.Quantity + quant) < 0) throw new Exception();
                        product.Quantity += quant;
                    }
                    db.SaveChanges();
                    Console.WriteLine("Inventory Updated.");
                    Continue();
                }
                catch
                {
                    CommandError();
                }
            }

        }
        #endregion

        public void ShowOrderHistory()
        {
            CurrentLocation();
            Console.WriteLine("Showing Order History:\n");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    var location = db.Locations.Find(UI.Location.Id);
                    var products = db.Products
                        .Where(product => product.Location.Id == location.Id)
                        .ToList();
                    var orders = db.Orders
                        .Where(order => order.Product.Location.Id == location.Id)
                        .ToList();
                    foreach (var order in orders)
                    {
                        Console.WriteLine(order);
                    }
                    Console.WriteLine();
                    Continue();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Unable to load order history. " + ex.Message);
                    Continue();

                }
            }
        }
        #endregion
    }
}