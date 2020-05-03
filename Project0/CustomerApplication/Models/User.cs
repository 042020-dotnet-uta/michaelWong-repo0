using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Models
{
    public class User
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public String LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public String Password { get; set; }
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