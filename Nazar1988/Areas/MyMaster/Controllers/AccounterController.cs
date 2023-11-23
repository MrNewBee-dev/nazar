using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using ReflectionIT.Mvc.Paging;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace Nazar1988.Areas.MyMaster.Controllers
{
   
    [Area("MyMaster")]
    public class AccounterController : Controller
    {
        private readonly NazarDbContext _context;
        private readonly _WalletService _userService;
        private readonly IConfiguration _configuration;
        public AccounterController(NazarDbContext context, _WalletService _userService,  IConfiguration _configuration)
        {
            this._configuration = _configuration;
            _context = context;
            this._userService = _userService;
            StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OJN40hx" +
                              "JjK5JbrxU+NrJ3E0OUAve6MDSIxK3504G4vSTqZezogz9ehm+xS8zUyh3tFhCWSvIoPFEEuqZTyO744uk+ezyGDj7C5" +
                              "jJQQjndNuSYeM+UdsAZVREEuyNFHLm7gD9OuR2dWjf8ldIO6Goh3h52+uMZxbUNal/0uomgpx5NklQZwVfjTBOg0xKB" +
                              "LJqZTDKbdtUrnFeTZLQXPhrQA5D+hCvqsj+DE0n6uAvCB2kNOvqlDealr9mE3y978bJuoq1l4UNE3EzDk+UqlPo8KwL" +
                              "1XM+o1oxqZAZWsRmNv4Rr2EXqg/RNUQId47/4JO0ymIF5V4UMeQcPXs9DicCBJO2qz1Y+MIpmMDbSETtJWksDF5ns6+" +
                              "B0R7BsNPX+rw8nvVtKI1OTJ2GmcYBeRkIyCB7f8VefTSOkq5ZeZkI8loPcLsR4fC4TXjJu2loGgy4avJVXk32bt4FFp" +
                              "9ikWocI9OQ7CakMKyAF6Zx7dJF1nZw");

        }
        public IActionResult PrintPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TempData["id"] = id;
            return View();
        }

          public IActionResult GetReport()
        {
            StiReport report = new StiReport();
            report.Load(StiNetCoreHelper.MapPath(this, "wwwroot/Reports/Report3.mrt"));
            int id = (int)TempData["id"];
            var FactorList = _context.OrderDetails.Where(x => x.OrderId == id).Select(y => new ShowOrderStimulViewModel
            {
                Count = y.Count,
                Sum = y.Order.Sum,
                Title = y.Product.ProductName,
                Price = y.Price,
                Address = y.Order.Address,
                OrderId = y.OrderId,
                QTY = y.Order.QTY
                

            }).ToList();
           
            report.RegBusinessObject("dt", FactorList);
            return StiNetCoreViewer.GetReportResult(this, report);
        }
        public IActionResult ViewerEvent()
        {
            return StiNetCoreViewer.ViewerEventResult(this);
        }

        //public IActionResult Print()
        //{
        //    StiReport report = new StiReport();
        //    report.Load(StiNetCoreHelper.MapPath(this, "wwwroot/Reports/Report.mrt"));
        //    var persons = _context.Orders.Select(x=>x.OrderId).ToList();
        //    report.RegData("dt", persons);
        //    return StiNetCoreViewer.GetReportResult(this, report);
        //}
        //public IActionResult ViewerEvent()
        //{
        //    return StiNetCoreViewer.ViewerEventResult(this);
        //}

        public IActionResult Index(int row = 10, int pages = 1)
        {
            //using (var context = new NazarDbContext())
            //using (var command = context.Database.GetDbConnection().CreateCommand())
            //{
            //    command.CommandText = "EXEC FactorId";
            //    context.Database.OpenConnection();
            //    using (var result = command.ExecuteReader())
            //    {

            //        // do something with result
            //    }
            //}
            var UserOrder = _context.View_User_Order.FromSqlRaw("Exec FactorId").ToList();
            if (UserOrder == null)
            {
                return NotFound();
            }
            
            ViewBag.Sum = UserOrder.Sum(x => x.Sum).ToString("#,0 تومان");
            //     var Accountproduct = _context.Orders.Select(x => new AccounterViewModels()
            //{
            //    Name = x.User.FirstName + "" + x.User.LastName,
            //    phone = x.User.PhoneNumber,
            //    CreateDate = x.CreateDate,
            //    SumPrice =x.Sum,
            //    Marhale = x.Marhale


            //}).ToList();
            var paginList = PagingList.Create(UserOrder, row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };

            return View(paginList);
        }

        //public  IActionResult Print()
        //{
        //    StiReport stiReport = new StiReport();
        //    stiReport.Load(StiNetCoreHelper.MapPath(this,"wwwroot/Reports/reports.mrt"));

        //    var FactorList = _context.OrderDetails.Where(x => x.OrderId == 1).Select(y => new ShowOrderStimulViewModel
        //    {
        //        Count = y.Count,
        //        Sum = y.Count * y.Price,
        //        Title = y.Product.ProductName,
        //        Price = y.Price,
        //        Address = y.Order.Address,
        //        OrderId = y.OrderId,
        //        QTY = y.Order.QTY


        //    }).ToList();

        //    if (FactorList == null)
        //    {
        //        return NotFound();
        //    }
        //    stiReport.RegData("dt", FactorList);

        //    return StiNetCoreViewer.GetReportResult(this,stiReport);
        //}
        //    public IActionResult ViewerEvent() {
        //    return StiNetCoreViewer.ViewerEventResult(this);
        //    }

        public  IActionResult ListBedehKaran(int row = 10, int pages = 1)
        {
            var paginList = PagingList.Create(_userService.GetWalletUserBedehi(), row, pages);
            paginList.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };


            return View(paginList);

        }
        public IActionResult JoziyateBedihi(int row = 10, int pages = 1) {


            return View();
        }
        public IActionResult Tasvie(string id)
        {

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Nazar1988ContextConnection")))
            {



                try
                {
                    SqlCommand cmd = new SqlCommand("JoinWalletAndUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = id;
                    cmd.Parameters.Add("@Out", SqlDbType.Bit).Direction = ParameterDirection.ReturnValue;
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int ret = int.Parse(cmd.Parameters["@Out"].Value.ToString());
                    if (ret == 1)
                    {
                        return RedirectToAction("ListBedehKaran");
                    }
                    else
                    {
                        return Content("نمیتواند اپدیت کند");

                    }
                }
                catch (Exception )
                {

                   

                }



            }
            return Content("مشکلی رخ داده");
        }

        public async Task<IActionResult> Details(int id) {

            var OrderDetails = await _context.OrderDetails.Where(x => x.OrderId == id).Select(y => new ShowOrderViewModel {
                Count = y.Count,
                Sum = y.Count * y.Price,
                Title = y.Product.ProductName,
                Price = y.Price

            }).ToListAsync();

            if (OrderDetails == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(OrderDetails);
        }
        public async Task<IActionResult> Accept(int id)
        {
            var Order = await _context.Orders.FindAsync(id);
            if (Order==null)
            {
                return NotFound();
            }
            if (Order.Marhale == 5)
            {
                Order.Marhale = 0;
            }
            else
            Order.Marhale = 1; 
            _context.Orders.Update(Order);
           await _context.SaveChangesAsync();

           return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id) {

            var order = await _context.Orders.FindAsync(id);
            
                var orderDetail = _context.OrderDetails.Where(od => od.OrderId == order.OrderId).ToList();
            if (orderDetail == null || order == null)
            {
                return NotFound();
            }
            foreach (var detail in orderDetail)
                {
                    var Product = _context.Products.Find(detail.ProductId);
                    Product.ProductTotal += detail.Count;
                    _context.Products.Update(Product);
                }
            
            _userService.ChargeWallet(order.UserId, order.Sum, "مرجوعی کالا", true);
            _context.Remove(order);
            _context.SaveChanges();


            return RedirectToAction("index");
        
        }

    }
}
