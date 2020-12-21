using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL_Core2.Models.MetaData
{
    interface ICustomerMetaData
    {
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public string LastName { get; set; }
    }
}
