using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

/// <summary>
/// Controls database access related to <c>User</c> objects.
/// Used by classes in the business logic layer.
/// </summary>
namespace CustomerApplication.DbAccess
{
    public class UserDb
    {
        #region Fields
        //Used for name validation.
        private static Regex NameRx = new Regex(@"^([^\W\d]|\s){1,50}$");
        //Used for password validation.
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

        /// <summary>
        /// Loads <c>User</c> from database by id.
        /// </summary>
        /// <param name="id">Id of the user being loaded.</param>
        /// <returns>User object.</returns>
        public User GetUser(int id)
        {

            using (var db = new CustomerApplicationContext())
            {
                return db.Users.Find(id);
            }

        }

        /// <summary>
        /// User login with id and password.
        /// </summary>
        /// <param name="userId">Id of the user logging in.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>User object if login successful.</returns>
        public User Login(String userId, String password)
        {
            using (var db = new CustomerApplicationContext())
            {
                var userIdInt = Int32.Parse(userId);
                return db.Users.Include(user => user.UserType)
                    .Single(user => user.Id == userIdInt && user.Password == password);
            }
        }

        /// <summary>
        /// Loads users from database that match input first name and last name.
        /// </summary>
        /// <param name="firstName">User first name for search.</param>
        /// <param name="lastName">User last name for search.</param>
        /// <returns>Collection of users matching search query.</param>
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