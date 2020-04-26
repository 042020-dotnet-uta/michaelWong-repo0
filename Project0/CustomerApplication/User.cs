using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public abstract class User
    {
        #region Fields
        private static Regex rx = new Regex(@"^([^\W\d]|\s)+$");
        public readonly String id;
        private String _firstName;
        public String firstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                if (rx.IsMatch(value)) _firstName = value.Trim();
                else throw new FormatException("Invalid first name input.");
            }
        }
        private String _lastName;
        public String lastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                if (rx.IsMatch(value)) _lastName = value.Trim();
                else throw new FormatException("Invalid last name input.");
            }
        }
        #endregion

        #region Constructors
        public User(String _first, String _last, String _id)
        {
            firstName = _first;
            lastName = _last;
            id = _id;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"Name:\t\t{firstName} {lastName}\n" +
                $"User ID:\t{id}";
        }
        public String ChangeFirstName(String _first)
        {
            try
            {
                firstName = _first;
            }
            catch (FormatException)
            {
                return "Invalid Input: Failed to changed first name.";
            }
            return "Successfully changed first name.";
        }
        public String ChangeLastName(String _last)
        {
            try
            {
                lastName = _last;
            }
            catch (FormatException)
            {
                return "Invalid Input: Unable to changed first name.";
            }
            return "Successfully changed last name.";
        }
        #endregion
    }
}