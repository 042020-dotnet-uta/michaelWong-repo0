using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The <c>AdminCommands</c> class.
/// Implements the <c>ICommands</c> interface.
/// Contains all business logic for commands issued by an Admin user.
/// </summary>
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
        /// <summary>
        /// Adds methods to <c>CommandsMain</c> which the user will have access to in the main menu.
        /// Adds the admin commands to the <c>CommandsMain</c>.
        /// </summary>
        public new void GetCommandsMain()
        {
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
        /// Creates a new instance of <c>UserBuilder</c> to insert a new user into the database.
        /// Method is called from <c>AddAdmin</c> and <c>AddCustomer</c>.
        /// </summary>
        /// <param name="userTypeId">An int which corresponds to the Id of UserType in the database.</param>
        public void AddUser(int userTypeId)
        {
            //New instance of UserBuilder. UserTypeId determines the UserType of the new user.
            User user = new UserBuilder().Build(userTypeId);

            //No user inserted into the database.
            if (user != null)
            {
                Console.Clear();
                Console.WriteLine("New user added.\n");
                Console.WriteLine(user + "\n");
                Continue();
            }
            //User inserted into the database.
            else
            {
                Console.Clear();
                Console.WriteLine("Failed to create new user.");
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
        /// Creates a new instance of <c>LocationBuilder</c> to insert a new location into the database.
        /// </summary>
        public void AddLocation()
        {
            //New instance of LocationBuilder. Returns instance of Location.
            Location location = new LocationBuilder().Build();
            //Location inserted into the database.
            if (location != null)
            {
                Console.Clear();
                Console.WriteLine("New location added.\n");
                Console.WriteLine(location + "\n");
            }
            Continue();
        }

        /// <summary>
        /// Displays all locations in the database. Accepts console input to delete a location from the database.
        /// </summary>
        public void RemoveLocation()
        {
            Console.WriteLine("Remove a Location (WARNING: Will remove products and order history):\n");
            Console.WriteLine("0:\tReturn");
            using (var db = new CustomerApplicationContext())
            {
                //Loads locations from database.
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
                    //Console input for Location Id.
                    var input = Int32.Parse(Console.ReadLine());
                    Console.Clear();

                    //0: Return
                    if (input == 0)
                    {
                        Continue();
                        return;
                    }
                    else
                    {
                        //Deletes location from database.
                        var location = db.Locations.Find(input);
                        db.Locations.Remove(location);
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
        /// <summary>
        /// Create a new instance of <c>ProductBuilder</c> to insert a new product into the database.
        /// </summary>
        public void AddProduct()
        {
            CurrentLocation();

            //New instance of ProductBuilder. Returns a Product instance which references the current location.
            Product product = new ProductBuilder().Build(UI.Location.Id);

            //Product inserted into database.
            if (product != null)
            {
                Console.Clear();
                Console.WriteLine("New product added.\n");
                Console.WriteLine(product + "\n");
            }
            Continue();
        }

        /// <summary>
        /// Displays all products in the location. Accepts console input to delete a product from the database.
        /// </summary>
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
                    //Console input.
                    var input = Int32.Parse(Console.ReadLine());

                    //0: Return
                    if (input == 0)
                    {
                        Continue();
                        return;
                    }
                    Console.Clear();

                    //Load current location from database.
                    db.Locations.Find(UI.Location.Id);

                    //Load product which match the input.
                    var product = db.Products.Find(input);

                    //Checks the product reference the current location.
                    if (product.Location.Id != UI.Location.Id) throw new Exception();

                    //Deletes product from current location.
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
            CurrentLocation();
            Console.WriteLine("0:\tReturn");
            DisplayInventory();
            Console.Write("\nExample: 142 12, 43 -20, 8890 30\nEnter [Id] [Quantity] of Products Separated By Commas:\n> ");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    //Console input.
                    var input = Console.ReadLine();

                    //0: Return
                    if (input == "0")
                    {
                        Continue();
                        return;
                    }
                    Console.Clear();

                    //Parses console input.
                    var splitInput = input.Split(",");
                    foreach (var splitElement in splitInput)
                    {
                        var quants = splitElement.Trim().Split(" ");
                        var id = Int32.Parse(quants[0]);
                        var quant = Int32.Parse(quants[1]);

                        //Load location and products from database.
                        var location = db.Locations.Find(UI.Location.Id);
                        var product = db.Products.Find(id);

                        //Checks all products reference the current location.
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

        /// <summary>
        /// Displays all orders in the database corresponding to products in the current location.
        /// </summary>
        public void ShowOrderHistory()
        {
            CurrentLocation();
            Console.WriteLine("Showing Order History:\n");
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    //Load location from database.
                    var location = db.Locations.Find(UI.Location.Id);

                    //Load products from database which reference the location.
                    var products = db.Products
                        .Where(product => product.Location.Id == location.Id)
                        .ToList();
                    
                    //Load orders from database which reference the location's products.
                    var orders = db.Orders
                        .Where(order => order.Product.Location.Id == location.Id)
                        .ToList();
                    
                    //Display orders.
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