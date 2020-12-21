using AutoLotDAL_Core2.Models;
using AutoLotDAL_Core2.Repos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoLotAPI_Core2.Controllers
{
    [Produces("application/json")]
    [Route("api/Inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        static IMapper mapper;
        private readonly IInventoryRepo _repo;

        public InventoryController(IInventoryRepo repo)
        {
            _repo = repo;
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Inventory, Inventory>().ForMember(x => x.Orders, opt => opt.Ignore());
            }).CreateMapper();
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetCars()
        {
            var inventories = await Task.Run(() => _repo.GetAll());
            return mapper.Map<List<Inventory>, List<Inventory>>(inventories);

        }

        // GET: api/Inventory/5
        [HttpGet("{id}", Name = "DisplayRoute")]
        public async Task<ActionResult> GetInventory([FromRoute] int id)
        {
            var inventory = await Task.Run(() => _repo.GetOne(id));

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Inventory, Inventory>(inventory));
        }

        // PUT: api/Inventory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory([FromRoute] int id, [FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _repo.Update(inventory);

            return NoContent();
        }

        // POST: api/Inventory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostInventory([FromBody] Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(inventory);

            return CreatedAtAction(/*"DisplayRoute"*/ nameof(GetInventory), new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{id}/{timestamp}")]
        public async Task<ActionResult> DeleteInventory([FromRoute]int id, [FromRoute] string timestamp)
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
