using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerApplication
{
    public class Login
    {
        #region Methods
        public User PromptLogin()
        {
            Console.Write("Login:\n\nEnter User ID:\n> ");
            String inputId = Console.ReadLine();
            Console.Write("Enter Password:\n> ");
            String password = Console.ReadLine();
            using(var db = new CustomerApplicationContext())
            {
                try
                {
                    var id = Int32.Parse(inputId);
                    return db.Users
                        .Include(u => u.UserType)
                        .Single(u => u.Id == id && u.Password == password);
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