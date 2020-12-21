using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL_Core2.Models.MetaData;
using Microsoft.AspNetCore.Mvc;

namespace AutoLotDAL_Core2.Models
{
    public partial class Inventory
    {
        public override string ToString()
        {
            return $"{this.PetName ?? "** No Name **"} is a {this.Color} {this.Make} with ID {this.Id}.";
        }

        [NotMapped]
        public string MakeColor => $"{Make} + ({Color})";
    }
}
