using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoLotDAL_Core2.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace AutoLotMVC_Core2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly string _baseUrl;

        public CustomerController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("CustomerServiceAddress").Value;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View("IndexWithViewComponent");
        }

        private async Task<Customer> GetCustomerRecord(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customer = JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
                return customer;
            }
            return null;
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = await GetCustomerRecord(id.Value);
            return customer != null ? (IActionResult)View(customer) : NotFound();
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, LastName")] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(customer);
                var response = await client.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = await GetCustomerRecord(id.Value);
            return customer != null ? (IActionResult)View(customer) : NotFound();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Id,Timestamp")]Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(customer);
            var response = await client.PutAsync($"{ _baseUrl}/{ customer.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = await GetCustomerRecord(id.Value);
            return customer != null ? (IActionResult)View(customer) : NotFound();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id,Timestamp")]Customer customer)
        {
            var client = new HttpClient();
            var timestampString = JsonConvert.SerializeObject(customer.Timestamp);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{customer.Id}/{timestampString}");
            await client.SendAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}