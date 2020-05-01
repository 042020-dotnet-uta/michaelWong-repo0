using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication
{
    public class UserType
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
        private String _name;
        [Required]
        [MaxLength(20)]
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private String _description;
        [Required]
        [MaxLength(200)]
        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public ICollection<User> Users { get; set; }
        #endregion
    }
}