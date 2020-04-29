using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerApplication
{
    public class Login
    {
        #region Constructors
        #endregion

        #region Methods
        public User PromptLogin()
        {
            Console.WriteLine("Enter User ID:");
            String id = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            String password = Console.ReadLine();
            //Send id and password to the database to see if user login is correct
            //Users[] users = db.query something
            using(var db = new CustomerApplicationContext())
            {
                try
                {
                    return db.Users.Single(u => u.ID == Int64.Parse(id) && u.Password == password);
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}