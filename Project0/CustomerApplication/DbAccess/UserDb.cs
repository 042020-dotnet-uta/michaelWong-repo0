using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

namespace CustomerApplication.DbAccess
{
    public class UserDb
    {
        #region Fields
        private static Regex NameRx = new Regex(@"^([^\W\d]|\s){1,50}$");
        private static Regex PasswordRx = new Regex(@"^[\p{Lu}\p{Ll}\p{Nd}]{8,50}$");
        private String _firstName;
        private String FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (NameRx.IsMatch(value.Trim())) _firstName = value.Trim();
                else throw new FormatException("Invalid first name input.");
            }
        }
        private String _lastName;
        private String LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (NameRx.IsMatch(value.Trim())) _lastName = value.Trim();
                else throw new FormatException("Invalid last name input.");
            }
        }
        private String _password;
        private String Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (PasswordRx.IsMatch(value)) _password = value;
                else throw new FormatException("Invalid password input. Must be at least 8 letters or numbers.");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validates console input to instantiate a new user and insert it into the database.
        /// </summary>
        /// <returns>
        /// null if console input fails validation.
        /// User object if new location inserted into database.
        /// </returns>
        public User Build(int userTypeId)
        {
            using (var db = new CustomerApplicationContext())
            {
                var userType = db.UserTypes.Find(userTypeId);
                Console.WriteLine($"Creating a New ({userType.Name}) User:\n");
                FirstNameInput();
                LastNameInput();
                PasswordInput();
                var user = db.Users.Add(new User(FirstName, LastName, Password, userType)).Entity;
                db.SaveChanges();
                return user;
            }

        }

        /// <summary>
        /// Gets console input for user first name.
        /// </summary>
        public void FirstNameInput()
        {
            Console.Write("Enter First Name:\n> ");
            FirstName = Console.ReadLine();
        }

        /// <summary>
        /// Gets console input for user last name.
        /// </summary>
        public void LastNameInput()
        {
            Console.Write("Enter Last Name:\n> ");
            LastName = Console.ReadLine();
        }

        /// <summary>
        /// Gets console input for user password.
        /// </summary>
        public void PasswordInput()
        {
            Console.Write("Enter Password:\n> ");
            Password = Console.ReadLine();
            Console.Write("Enter Password For Confirmation:\n> ");
            if (Console.ReadLine() != Password) throw new FormatException("Passwords don't match.");
        }

        public User GetUser(int id)
        {

            using (var db = new CustomerApplicationContext())
            {
                return db.Users.Find(id);
            }

        }

        public ICollection<User> GetUsers()
        {

            using (var db = new CustomerApplicationContext())
            {
                return db.Users
                    .AsNoTracking()
                    .ToList();
            }

        }

        public User Login(String userId, String password)
        {
            using (var db = new CustomerApplicationContext())
            {
                var userIdInt = Int32.Parse(userId);
                return db.Users.Include(user => user.UserType)
                    .Single(user => user.Id == userIdInt && user.Password == password);
            }
        }

        public ICollection<User> SearchByName(string firstName, string lastName)
        {
            using (var db = new CustomerApplicationContext())
            {
                return db.Users
                    .AsNoTracking()
                    .Where(user => user.FirstName == firstName && user.LastName == lastName)
                    .Include(user => user.UserType)
                    .ToList();
            }
        }
        #endregion
    }
}