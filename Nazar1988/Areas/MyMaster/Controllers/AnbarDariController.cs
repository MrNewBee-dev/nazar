using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Area("MyMaster")]
    public class AnbarDariController : Controller
    {
        private readonly NazarDbContext _context;

        public AnbarDariController(NazarDbContext context)
        {
            _context = context;

        }
        public IActionResult Index(int row = 10, int pages = 1)
        {
            var AnbarFactor = _context.View_User_Order.FromSqlRaw("EXEC AnbarFactor").ToList();
            if (AnbarFactor == null)
            {
                return NotFound();
            }
            var paginList = PagingList.Create(AnbarFactor, row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };
            return View(paginList);
        }

        
        public async Task<IActionResult> Accept(int id)
        {
            var Order = await _context.Orders.FindAsync(id);
            Order.Marhale = 2;
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Details(int id)
        {

            var OrderDetails = await _context.OrderDetails.Where(x => x.OrderId == id).Select(y => new ShowOrderViewModel
            {
                Count = y.Count,
                Sum = y.Count * y.Price,
                Title = y.Product.ProductName,
                Price = y.Price

            }).ToListAsync();


            return View(OrderDetails);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var Order = await _context.Orders.FindAsync(id);
            Order.Marhale = 6; //marjoie anbar be hessabdari 
            _context.Orders.Update(Order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

            

        }

    }
}
