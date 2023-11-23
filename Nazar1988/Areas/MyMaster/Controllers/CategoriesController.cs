using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Models;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
    
    public class CategoriesController : Controller
    {
        private readonly NazarDbContext _context;

        public CategoriesController(NazarDbContext context)
        {
            _context = context;
        }

        // GET: MyMaster/Categories
        public async Task<IActionResult> Index()
        {
            var nazarDbContext = _context.Categories.Include(c => c.category);
            return View(await nazarDbContext.ToListAsync());
        }

        // GET: MyMaster/Categories/Details/5
      

        // GET: MyMaster/Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: MyMaster/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,ParentCategoryID")] Category category2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", category2.ParentCategoryID);
            return View(category2);
        }

        // GET: MyMaster/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", category.ParentCategoryID);
            return View(category);
        }

        // POST: MyMaster/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName,ParentCategoryID")] Category category1)
        {
            if (id != category1.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category1.CategoryID))
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
            ViewData["ParentCategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", category1.ParentCategoryID);
            return View(category1);
        }

        // GET: MyMaster/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.category)
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: MyMaster/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
