using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL_Core2.Models.MetaData
{
    public interface IInventoryMetaData
    {
        [Display(Name = "Pet Name")]
        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public string PetName { get; set; }

        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public string Make { get; set; }

        [MaxLength(50, ErrorMessage = "Please enter a value less than 50 characters long.")]
        public string Color { get; set; }
    }
}
