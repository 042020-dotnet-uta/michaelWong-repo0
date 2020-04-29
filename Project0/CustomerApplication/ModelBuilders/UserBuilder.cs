using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class UserBuilder
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

        #region Constructor
        public UserBuilder() { }
        #endregion

        #region Methods
        public User Build(int userTypeId)
        {
            try
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
            catch
            {
                return null;
            }
        }


        public void FirstNameInput()
        {
            Console.Write("Enter First Name:\n> ");
            FirstName = Console.ReadLine();
        }

        public void LastNameInput()
        {
            Console.Write("Enter Last Name:\n> ");
            LastName = Console.ReadLine();
        }

        public void PasswordInput()
        {
            Console.Write("Enter Password:\n> ");
            Password = Console.ReadLine();
            Console.Write("Enter Password For Confirmation:\n> ");
            if (Console.ReadLine() != Password) throw new FormatException("Passwords don't match.");
        }
        #endregion
    }
}