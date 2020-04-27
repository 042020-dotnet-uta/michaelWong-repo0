using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class User
    {
        #region Fields
        private static Regex Rx = new Regex(@"^([^\W\d]|\s)+$");
        [Key]
        public long ID{get; private set;}
        private String _firstName;
        public String FirstName
        {
            get
            {
                return _firstName;
            }
            private set
            {
                if (Rx.IsMatch(value)) _firstName = value.Trim();
                else throw new FormatException("Invalid first name input.");
            }
        }
        private String _lastName;
        public String LastName
        {
            get
            {
                return _lastName;
            }
            private set
            {
                if (Rx.IsMatch(value)) _lastName = value.Trim();
                else throw new FormatException("Invalid last name input.");
            }
        }
        public String Password{get; private set;}
        public String Type{get; private set;}
        //public ICollection<Order> Orders{get;}
        #endregion

        #region Constructors
        public User(){}
        public User(String first, String last, long id, String type)
        {
            FirstName = first;
            LastName = last;
            ID = id;
            Type = type;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"Name:\t\t{FirstName} {LastName}\n" +
                $"User ID:\t{ID}";
        }
        public String ChangeFirstName(String first)
        {
            try
            {
                FirstName = first;
            }
            catch (FormatException)
            {
                return "Invalid Input: Failed to changed first name.";
            }
            return "Successfully changed first name.";
        }
        public String ChangeLastName(String last)
        {
            try
            {
                LastName = last;
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