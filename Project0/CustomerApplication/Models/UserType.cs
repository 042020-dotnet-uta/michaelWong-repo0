using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApplication.Models
{
    public class UserType
    {
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public String Name { get; set; }
        [Required]
        [MaxLength(200)]
        public String Description { get; set; }
        public ICollection<User> Users { get; set; }
        #endregion
    }
}