using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using ZarinpalSandbox;

namespace Nazar1988.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly NazarDbContext _context;
        private readonly Nazar1988Context _Wallet;
        private readonly UserManager<Nazar1988User> _user;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(NazarDbContext context, Nazar1988Context _Wallet, UserManager<Nazar1988User> _user)
        {
            _context = context;
            this._Wallet = _Wallet;
            this._user = _user;
        }
        public IActionResult Index(int row = 10, int pages = 1 )
        {
             

            var Products =  _context.View_Products_Discounts.FromSqlRaw("exec Product_Discount");

            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
                ViewBag.role = roles.Contains("توزیع کننده");

            //var UserId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var UserFound = await _Wallet.Users.Where(x=>x.Id == UserId).FirstOrDefaultAsync();
            //if (UserId !=null)
            //{
            //    var Rolefound = await _user.GetRolesAsync(UserFound);
            //    ViewBag.role = Rolefound.Contains("توزیع کننده ");
            //}


            var pagingModel = PagingList.Create(Products, row, pages);
            pagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row }
            };
            
            return View(pagingModel);
            
        }
        public IActionResult OnlinePayment(int id)
        {


            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();

                var order = _context.Orders.Find(id);
                var details = _context.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();



                var payment = new Payment(order.Sum);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinaly = true;
                    //Product product;                    
                    _context.Orders.Update(order);
                    //foreach (var item in details)
                    //{
                    //    product = _context.Products.FirstOrDefault(x => x.ProductID == item.ProductId);
                    //    product.ProductTotal -= item.Count;
                    //    _context.Update(product);
                    //}

                    _context.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View();
                }

            }
            else {
                ViewBag.Error = "مشکلی در پرداخت رخداده است.";
                return View();
            
            }

            return NotFound();
        }

        public async Task<IActionResult> OnlinePaymentWallet(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();

                var order = _Wallet.wallets.Find(id);
                if (order == null)
                {
                    return NotFound();
                }



                var payment = new Payment(order.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsPay = true;
                    //Product product;                    
                    _Wallet.wallets.Update(order);
                    //foreach (var item in details)
                    //{
                    //    product = _context.Products.FirstOrDefault(x => x.ProductID == item.ProductId);
                    //    product.ProductTotal -= item.Count;
                    //    _context.Update(product);
                    //}

                   await _Wallet.SaveChangesAsync();
                    ViewBag.code = res.RefId;
                    return View();
                }

            }

            return NotFound();
        }
        public IActionResult Details(int id)

        {
            var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            ViewBag.role = roles.Contains("توزیع کننده ");

            var Products = _context.View_Products_Discounts
           .FirstOrDefault( r => r.ProductID == id);
            if (Products ==null)
            {
                return NotFound();
            }
            return View(Products);

            //  var test = Path.GetFileName(System.Text.Json.JsonSerializer.Deserialize<List<string>>(_context.Products.First().ImagePath).First());

            //var Products = _context.Products.Select(r => new ProductViewModelViews
            //{
            //    Price = r.Price,
            //    ProductDescription = r.ProductDescription,
            //    ProductName = r.ProductName,
            //    Image = r.ImagePath

            //}).ToListAsync();
            //var pagingModel = PagingList.Create(await Products, row, pages);
            //pagingModel.RouteValue = new RouteValueDictionary
            //{
            //    {"row",row }
            //};

          

        }


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
