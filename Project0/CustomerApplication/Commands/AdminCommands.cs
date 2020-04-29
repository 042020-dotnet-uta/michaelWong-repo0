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
        }

        #region Main Menu
        public void AddUser(String type)
        {
            User user = new UserBuilder().BuildUser(type);
            if (user != null)
            {
                Console.Clear();
                Console.WriteLine("New user added.\n");
                Console.WriteLine(user + "\n");
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public void AddAdmin()
        {
            AddUser("admin");
        }

        public void AddCustomer()
        {
            AddUser("customer");
        }

        public void AddLocation()
        {
            Location location = new LocationBuilder().BuildLocation();
            if (location != null)
            {
                Console.Clear();
                Console.WriteLine("New location added.\n");
                Console.WriteLine(location + "\n");
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public void RemoveLocation()
        {
            Console.WriteLine("Removing a Location\n");
            Console.WriteLine("0: Return");
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
                    var input = Int64.Parse(Console.ReadLine());
                    Console.Clear();
                    if (input == 0)
                    {
                        GoBack();
                        return;
                    }
                    else
                    {
                        var location = db.Locations.Find(input);
                        var products = db.Products
                            .Where(p => p.LocationID == location.ID)
                            .ToList();
                        db.Locations.Remove(location);
                        db.Products.RemoveRange(products);
                        db.SaveChanges();
                        Console.WriteLine("Location and products removed. Press enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
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
            Product product = new ProductBuilder(UI.Location.ID).BuildProduct();
            if (product != null)
            {
                Console.Clear();
                Console.WriteLine("New product added.\n");
                Console.WriteLine(product + "\n");
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public void RemoveProduct()
        {
            CurrentLocation();
            Console.WriteLine("0: Return");
            DisplayInventory();
            Console.Write("\nEnter Product ID:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    var input = Int64.Parse(Console.ReadLine());
                    var product = db.Products.Find(input);
                    if (product.LocationID != UI.Location.ID) throw new Exception();
                    db.Products.Remove(product);
                    db.SaveChanges();
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
            Console.WriteLine("0: Return");
            DisplayInventory();
            Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter ID Quantity of Products Separated By Commas:\n> ");
            try
            {
                var input = Console.ReadLine();
                Console.Clear();
                if (input == "0")
                {
                    GoBack();
                    return;
                }
                else
                {
                    using (var db = new CustomerApplicationContext())
                    {
                        var splitInput = input.Split(",");
                        foreach (var splitElement in splitInput)
                        {
                            var quants = splitElement.Trim().Split(" ");
                            var id = Int64.Parse(quants[0]);
                            var quant = Int32.Parse(quants[1]);
                            var product = db.Products.Find(id);
                            if (product.LocationID != UI.Location.ID || (product.Quantity + quant) < 0) throw new Exception();
                            product.Quantity += quant;
                        }
                        db.SaveChanges();
                        Console.WriteLine("Inventory Updated. Press enter to continue.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
            catch
            {
                CommandError();
            }
        }
        #endregion
        #endregion
    }
}