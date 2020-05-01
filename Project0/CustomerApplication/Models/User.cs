using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class User
    {
        #region Fields
        private int _id;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private String _firstName;
        [Required]
        [MaxLength(50)]
        public String FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        private String _lastName;
        [Required]
        [MaxLength(50)]
        public String LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        private String _password;
        [Required]
        [MaxLength(50)]
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        [Required]
        public UserType UserType { get; set; }
        public ICollection<Order> Orders { get; set; }
        #endregion

        #region Constructors
        private User() { } //For Entity Framework
        public User(String firstName, String lastName, String password, UserType userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            UserType = userType;
        }
        #endregion

        #region Methods
        public override String ToString()
        {
            return $"User Id:\t{Id}\n"
                + $"Name:\t\t{FirstName} {LastName}\n"
                + $"User Type:\t{UserType.Name}";
        }
        #endregion
    }
}