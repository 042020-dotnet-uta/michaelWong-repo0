using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class UserBuilder
    {
        #region Fields
        private static Regex NameRx = new Regex(@"^([^\W\d]|\s)+$");
        private static Regex PasswordRx = new Regex(@"^[\p{Lu}\p{Ll}\p{Nd}]{8,}$");
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
                else throw new FormatException("Invalid password input. Must be at least 8 Latin alphabet characters or Arabic numbers.");
            }
        }
        private String Type;
        #endregion

        #region Constructor
        public UserBuilder(String type)
        {
            Type = type;
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
                    FirstName = str;
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
                    LastName = str;
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
                    Password = str;
                    check = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    check = true;
                }
            } while (check);

            Console.WriteLine();
            
            switch(Type)
            {
                case "admin":
                    //TODO: save password
                    return new Admin(FirstName, LastName, 7492837124);
                default:
                    //TODO: save password
                    return new Customer(FirstName, LastName, 7492837124);
            }
        }
        #endregion
    }
}