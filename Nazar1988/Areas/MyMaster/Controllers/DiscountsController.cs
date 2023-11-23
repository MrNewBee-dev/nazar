using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Models;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
  //  [Authorize(Roles = "مدیر")]
    public class DiscountsController : Controller
    {
        private readonly NazarDbContext _context;

        public DiscountsController(NazarDbContext context)
        {
            _context = context;
        }

        // GET: MyMaster/Discounts
        public async Task<IActionResult> Index(int row = 10, int pages = 1)
        {
            var nazarDbContext = _context.Discounts.Include(d => d.ProductIdies);
            var paginList = PagingList.Create(await nazarDbContext.ToListAsync(), row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };

            return View(paginList);
        }

        // GET: MyMaster/Discounts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var discount = await _context.Discounts
        //        .Include(d => d.ProductIdies)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (discount == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(discount);
        //}

        // GET: MyMaster/Discounts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ID", "ID");
            return View();
        }

        // POST: MyMaster/Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,DiscountPercent,StartDate,EndtDate")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ID", "ID", discount.ProductId);
            return View(discount);
        }

        // GET: MyMaster/Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ID", "ID", discount.ProductId);
            return View(discount);
        }

        // POST: MyMaster/Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,DiscountPercent,StartDate,EndtDate")] Discount discount)
        {
            if (id != discount.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.ProductId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ID", "ID", discount.ProductId);
            return View(discount);
        }

        // GET: MyMaster/Discounts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var discount = await _context.Discounts
        //        .Include(d => d.ProductIdies)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (discount == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(discount);
        //}

        // POST: MyMaster/Discounts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var discount = await _context.Discounts.FindAsync(id);
        //    _context.Discounts.Remove(discount);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool DiscountExists(int id)
        {
            return _context.Discounts.Any(e => e.ProductId == id);
        }
    }
}
