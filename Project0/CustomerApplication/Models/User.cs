using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class User
    {
        #region Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ID{get; set;}
        public String FirstName{get; set;}
        public String LastName{get; set;}
        public String Password{get; set;}
        public String Type{get; set;}
        #endregion

        #region Constructors
        public User(){}
        public User(String first, String last, String type, String password)
        {
            FirstName = first;
            LastName = last;
            Type = type;
            Password = password;

        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"User ID:\t{ID}\n"
                + $"Name:\t\t{FirstName} {LastName}\n"
                + $"User Type:\t{Type}";
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