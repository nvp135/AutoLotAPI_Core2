using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL_Core2.Models.Base;
using AutoLotDAL_Core2.Models.MetaData;

namespace AutoLotDAL_Core2.Models
{
    [Table("Inventory")]
    public partial class Inventory : EntityBase
    {
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        [StringLength(50)]
        public string Make { get; set; }

        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        [StringLength(50)]
        public string Color { get; set; }

        [Display(Name = "Pet Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        [StringLength(50)]
        public string PetName { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
