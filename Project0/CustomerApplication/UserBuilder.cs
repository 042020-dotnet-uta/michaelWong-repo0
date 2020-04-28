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
        #endregion

        #region Methods
        public User BuildUser()
        {
            return this.BuildUser("customer");
        }

        public User BuildUser(String type)
        {
            Console.WriteLine("Creating a New User:");
            try
            {
                FirstNameInput();
                LastNameInput();
                PasswordInput();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return new User(FirstName, LastName, type, Password);
        }


        public void FirstNameInput()
        {
            Console.WriteLine("Enter First Name:");
            FirstName = Console.ReadLine();
        }

        public void LastNameInput()
        {
            Console.WriteLine("Enter Last Name:");
            FirstName = Console.ReadLine();
        }

        public void PasswordInput()
        {
            Console.WriteLine("Enter Password:");
            Password = Console.ReadLine();
            Console.WriteLine("Enter Password For Confirmation:");
            if (Console.ReadLine() != Password) throw new FormatException("Passwords don't match.");
        }
        #endregion
    }
}