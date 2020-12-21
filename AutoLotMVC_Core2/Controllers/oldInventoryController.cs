using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoLotDAL_Core2.Repos;
using AutoLotDAL_Core2.Models;

namespace AutoLotMVC_Core2.Controllers
{
    public class oldInventoryController : Controller
    {
        private readonly IInventoryRepo _context;

        public oldInventoryController(IInventoryRepo context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            //return View(_context.GetAll());
            return View("IndexWithViewComponent");
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return View(inventory);
            }
            try
            {
                _context.Add(inventory);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create the record: {ex.Message}");
                return View(inventory);
            }
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Make,Color,PetName,Id,Timestamp")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(inventory);
            }
            try
            {
                _context.Update(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. Another user has update it. : {ex.Message}");
                return View(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = _context.GetOne(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("Id,Timestamp")] Inventory inventory)
        {
            try
            {
                _context.Delete(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. Another user updated the record, {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
