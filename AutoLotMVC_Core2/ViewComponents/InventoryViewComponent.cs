using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoLotDAL_Core2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace AutoLotMVC_Core2.ViewComponents
{
    public class InventoryViewComponent : ViewComponent
    {
        private readonly string _baseUrl;
        public InventoryViewComponent(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("InventoryServiceAddress").Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}");
            if (response.IsSuccessStatusCode)
            {
                var items = JsonConvert.DeserializeObject<List<Inventory>>(await response.Content.ReadAsStringAsync());
                return View("InventoryPartialView", items);
            }

            //var cars = _repo.GetAll(x => x.Make, true);
            //if (cars != null)
            //{
            //    return View("InventoryPartialView", items);
            //}
            return new ContentViewComponentResult("Unable to locate records.");
        }
    }
}
