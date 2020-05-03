using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;
using CustomerApplication.DbAccess;

/// <summary>
/// The <c>Login</c> class.
/// Console input for User login or inserting new Customer users into the database.
/// </summary>
namespace CustomerApplication.Controllers
{
    public class Login
    {
        #region Methods
        /// <summary>
        /// Displays the Login menu.
        /// Accepts user input from the console to <c>PromptLogin</c> and <c>CreateNewUser</c>.
        /// </summary>
        /// <returns>
        /// <c>User</c> object if successful login.
        /// null object is CreateNewUser or unsuccessful login.
        /// </returns>
        public User DisplayLogin()
        {
            Console.WriteLine("Login or Register:\n\n0:\tLogin\n1:\tNew User");
            try
            {
                //Console input.
                Console.Write("\n> ");
                int input = Int32.Parse(Console.ReadLine());
                Console.Clear();
                switch (input)
                {
                    //Prompts user for login. Login using Id and Password.
                    case 0:
                        User user = PromptLogin();

                        //Unsuccessful login.
                        if (user == null)
                        {
                            Console.WriteLine("Login failed.\nPress any key to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            return null;
                        }

                        //Successful login.
                        else
                        {
                            Console.WriteLine("Successful login.\n");
                            Console.WriteLine(user);
                            Console.WriteLine("\nPress any key to continue.");
                            Console.ReadLine();
                            Console.Clear();
                            return user;
                        }

                    //Prompts user for new login information. Inserts a new user into the database.
                    case 1:
                        return CreateNewUser();

                    //Unrecognized command.
                    default:
                        throw new FormatException();
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Invalid command. Press enter to continue.");
                Console.ReadLine();
                Console.Clear();
                return null;
            }
        }

        /// <summary>
        /// Accepts user input from the console for user login.
        /// User logs in with Id and Password.
        /// </summary>
        /// <returns>
        /// <c>User</c> object if successful login.
        /// null object if unsuccessful login.
        /// </returns>
        public User PromptLogin()
        {
            //Console input for login.
            Console.Write("Login:\n\nEnter User Id:\n> ");
            String inputId = Console.ReadLine();
            Console.Write("Enter Password:\n> ");
            String password = Console.ReadLine();
            using (var db = new CustomerApplicationContext())
            {
                try
                {
                    //Tries to load user from database matching login information.
                    var id = Int32.Parse(inputId);
                    Console.Clear();
                    return db.Users
                        .Include(u => u.UserType)
                        .Single(u => u.Id == id && u.Password == password);
                }
                catch
                {
                    Console.Clear();
                    return null;
                }
            }
        }

        /// <summary>
        /// Creates new instance of UserBuilder to create a new Customer.
        /// </summary>
        /// <returns>
        /// null object.
        /// </returns>
        public User CreateNewUser()
        {
            try
            {
                //New instance of UserBuilder. Returns user of Customer type.
                var user = new UserDb().Build(2);

                //User inserted into the database.
                Console.Clear();
                Console.WriteLine("New user created. Use id and password to sign in.\n");
                Console.WriteLine(user);
                Console.WriteLine("\nPress enter to continue.");

            }
            catch (System.Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Failed to create a new user.\nPress enter to continue.");
            }
            Console.ReadLine();
            Console.Clear();
            return null;
        }
        #endregion
    }
}