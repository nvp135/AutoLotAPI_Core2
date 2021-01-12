using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.EF;

namespace AutoLotDAL_Core2.Repos
{
    public class CustomersRepo : BaseRepo<Customer>, ICustomersRepo
    {
        public CustomersRepo(AutoLotContext context) : base(context)
        { }
        
    }
}
