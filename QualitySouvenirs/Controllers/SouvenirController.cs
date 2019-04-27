using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Controllers
{
    public class SouvenirController : Controller
    {
        private readonly SouvenirContext _context;

        public SouvenirController(SouvenirContext context)
        {
            _context = context;
        }

        // GET: Souvenirs
        public async Task<IActionResult> Index()
        {
            var souvenirContext = _context.Souvenirs.Include(s => s.Category).Include(s => s.Supplier);
            return View(await souvenirContext.ToListAsync());
        }

        public async Task<IActionResult> CategoryView(int? id, string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            

            var souvenirs = from s in _context.Souvenirs.Where(s => s.CategoryID == id)
                            select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                souvenirs = souvenirs.Where(s => s.Name.Contains(searchString)
                || s.Price.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    souvenirs = souvenirs.OrderBy(s => s.Name);
                    break;
                case "Price":
                    souvenirs = souvenirs.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    souvenirs = souvenirs.OrderByDescending(s => s.Price);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Souvenir>.CreateAsync(souvenirs.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<ActionResult> ProductView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.SingleOrDefaultAsync(s => s.SouvenirID == id);

            return View(souvenir);
        }


        // GET: Souvenirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SouvenirID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }



        // GET: Souvenirs/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name");
            return View();
        }

        // POST: Souvenirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SouvenirID,Name,Description,SupplierID,CategoryID,Price,ImagePath")] Souvenir souvenir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(souvenir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.FindAsync(id);
            if (souvenir == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // POST: Souvenirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SouvenirID,Name,Description,SupplierID,CategoryID,Price,ImagePath")] Souvenir souvenir)
        {
            if (id != souvenir.SouvenirID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(souvenir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SouvenirExists(souvenir.SouvenirID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "Name", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "Name", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SouvenirID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // POST: Souvenirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var souvenir = await _context.Souvenirs.FindAsync(id);
            _context.Souvenirs.Remove(souvenir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.SouvenirID == id);
        }
    }
}
