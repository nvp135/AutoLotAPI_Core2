using System.Collections.Generic;
using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.EF;

namespace AutoLotDAL_Core2.Repos
{
    public class InventoryRepo : BaseRepo<Inventory>, IInventoryRepo
    {
        public InventoryRepo(AutoLotContext context) : base(context)
        { }

        public override List<Inventory> GetAll()
        {
            return GetAll(x => x.PetName, true).ToList();
        }

        public List<Inventory> GetPinkCars()
        {
            return GetSome(x => x.Color == "Pink");
        }

        public List<Inventory> GetRelatedData()
        {
            return null;
            /*return Context.Cars.FromSql("Select * from Inventory")
                .Include(x => x.Orders)
                .ThenInclude(x => x.Customer).ToList();*/
        }

        public List<Inventory> Search(string searchString)
        {
            return Context.Cars.Where(c => Functions.Like(c.PetName,
                            $"%{searchString}%")).ToList();
        }
    }
}
