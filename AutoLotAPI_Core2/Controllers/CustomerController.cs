using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoLotDAL_Core2.Repos;
using AutoLotDAL_Core2.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace AutoLotAPI_Core2.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersRepo _repo;
        static IMapper mapper;
        public CustomerController(ICustomersRepo repo)
        {
            _repo = repo;
            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Customer, Customer>().ForMember(x => x.Orders, opt => opt.Ignore());
            }).CreateMapper();
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = _repo.GetAll();
            return mapper.Map<List<Customer>, List<Customer>>(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await Task.Run(() => _repo.GetOne(id));

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Customer, Customer>(customer));
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute]int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _repo.Update(customer);

            return NoContent();

        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(customer);
            return CreatedAtAction(/*"DisplayRoute"*/ nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}/{timestamp}")]
        public async Task<ActionResult> DeleteCustomer([FromRoute]int id, [FromRoute] string timestamp)
        {
            if (!timestamp.StartsWith("\""))
            {
                timestamp = $"\"{timestamp}\"";
            }
            var ts = JsonConvert.DeserializeObject<byte[]>(timestamp);
            _repo.Delete(id, ts);

            return Ok();
        }

    }
}
