using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix.Areas.Identity.Data;
using Phoenix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Controllers
{
    public class CarsController : Controller
    {
        private readonly PhoenixContext _context;

        public CarsController(PhoenixContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string carMark, string carBrand, string searchString)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'CarContext.Car' is null.");
            }

            IQueryable<string> markQuery = from c in _context.Car orderby c.Mark select c.Mark;
            IQueryable<string> brandQuery = from c in _context.Car orderby c.Brand select c.Brand;

            var cars = from c in _context.Car select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Name!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(carBrand))
            {
                if (!String.IsNullOrEmpty(carMark))
                {
                    cars = cars.Where(x => x.Mark == carMark && x.Brand == carBrand);
                }
                else
                {
                    cars = cars.Where(x => x.Brand == carBrand);
                }
            } else
            {
                if (!String.IsNullOrEmpty(carMark))
                {
                    cars = cars.Where(x => x.Mark == carMark);
                }
            }

            

            var carMarkVM = new CarViewModel
            {
                Brands = new SelectList(await brandQuery.Distinct().ToListAsync()),
                Marks = new SelectList(await markQuery.Distinct().ToListAsync()),
                Cars = await cars.ToListAsync()
            };

            return View(carMarkVM);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Policy = "Dealer")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Brand,Mark,TechSpecs,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.Brand != null && (User.IsInRole(car.Brand) || User.IsInRole("Admin")))
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Policy = "Dealer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null || (!User.IsInRole(car.Brand) || !User.IsInRole("Admin")))
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Brand,Mark,TechSpecs,Price")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Policy = "Dealer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null || (!User.IsInRole(car.Brand) || !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
