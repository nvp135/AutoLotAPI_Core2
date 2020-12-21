using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL_Core2.Models.Base;
using AutoLotDAL_Core2.Models.MetaData;

namespace AutoLotDAL_Core2.Models
{
    public class Customer : EntityBase
    {
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        [StringLength(50)]
        public string LastName { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
        
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
