﻿using System;
using System.Linq;

namespace CustomerApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();
            UserTerminal ui = new UserTerminal();
            ui.Run();

            //Set up. Create the first admin user.
            /*
            using(var db = new CustomerApplicationContext())
            {   
                UserType admin = db.UserTypes.Add(new UserType {Description = "Administrator user. Access to tools to create new users, locations and products. Able to manage inventories.", Name = "Admin"}).Entity;
                db.UserTypes.Add(new UserType {Description = "Customer usercess to viewing locations and placing orders.", Name = "Customer"});
                db.SaveChanges();
                db.Users.Add(new User("System", "Admin", "password", admin));
                db.SaveChanges();
            }
            */
            /*
            using(var db = new CustomerApplicationContext())
            {
                Location location = db.Locations.Find(1);
                Console.WriteLine(location);
                var product = new Product("Something", "1.00", location);
                Console.WriteLine(product);
                db.Locations.Add(location);
                db.SaveChanges();
                db.Products.Add(product);
                db.SaveChanges();
            }
            */

        }
    }
}