using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class UserBuilder
    {
        #region Fields
        private static Regex nameRx = new Regex(@"^([^\W\d]|\s)+$");
        private static Regex passwordRx = new Regex(@"^[\p{Lu}\p{Ll}\p{Nd}]{8,}$");
        private String _firstName;
        private String firstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (nameRx.IsMatch(value.Trim())) _firstName = value.Trim();
                else throw new FormatException("Invalid first name input.");
            }
        }
        private String _lastName;
        private String lastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (nameRx.IsMatch(value.Trim())) _lastName = value.Trim();
                else throw new FormatException("Invalid last name input.");
            }
        }
        private String _password;
        private String password
        {
            get
            {
                return _password;
            }
            set
            {
                if (passwordRx.IsMatch(value)) _password = value;
                else throw new FormatException("Invalid password input. Must be at least 8 Latin alphabet characters or Arabic numbers.");
            }
        }
        private String type;
        #endregion

        #region Constructor
        public UserBuilder(String _type)
        {
            type = _type;
        }
        #endregion
        
        #region Methods
        public User BuildUser()
        {
            Console.WriteLine("Creating a new user:");
            bool check = true;
            do
            {
                try
                {
                    Console.WriteLine("\nEnter First Name:");
                    String str = Console.ReadLine();
                    firstName = str;
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    check = true;
                }
            } while (check);

            do
            {
                try
                {
                    Console.WriteLine("\nEnter Last Name:");
                    String str = Console.ReadLine();
                    lastName = str;
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    check = true;
                }
            } while (check);

            do
            {
                try
                {
                    Console.WriteLine("\nEnter Password:");
                    String str = Console.ReadLine();
                    password = str;
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    check = true;
                }
            } while (check);

            Console.WriteLine();
            
            switch(type)
            {
                case "admin":
                    //TODO: save password
                    return new Admin(firstName, lastName, "7492837124");
                default:
                    //TODO: save password
                    return new Customer(firstName, lastName, "7492837124");
            }
        }
        #endregion
    }
}