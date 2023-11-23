using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Nazar1988.Areas.Identity.Data.Services;
using Nazar1988.Models;
using Nazar1988.Models.ViewModels;
using ZarinpalSandbox;

namespace Nazar1988.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private NazarDbContext _ctx;
        private readonly _WalletService _userService;
        private bool tozi;
        public OrdersController(NazarDbContext ctx, _WalletService userService)
        {
            _userService = userService;
            _ctx = ctx;
           
        }
        public IActionResult AddToCart(int id)
        {
            tozi = ((ClaimsIdentity)User.Identity).Claims
              .Where(c => c.Type == ClaimTypes.Role)
              .Select(c => c.Value).Contains("توزیع کننده");
            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = _ctx.Products.Include(x => x.DiscountId).Where(p=>p.ProductID == id).First();
            if (product == null)
            {
                return NotFound();
            }
            Order order = _ctx.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinaly);
            if (product.ProductTotal > 30 || tozi)
            {
                int priceProduct = (tozi) ? product.PriceToziKonande : product.Price;
                byte MarhaleToziKanande =  (byte)((tozi) ? 5 : 0);
                if (order == null)
                {
                    order = new Order()
                    {
                        UserId = CurrentUserID,
                        CreateDate = DateTime.Now,
                        IsFinaly = false,
                        Sum = 0
                        ,QTY=0,
                        Marhale = MarhaleToziKanande //agar 5 bood tozi konande kharide ast
                    };
                    _ctx.Orders.Add(order);
                    _ctx.SaveChanges();
                    _ctx.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,

                        Price = (product.DiscountId.StartDate < DateTime.Now && product.DiscountId.EndtDate > DateTime.Now) ? priceProduct - (product.DiscountId.DiscountPercent * priceProduct) /100 : priceProduct,
                        ProductId = id

                    });
                    if (!tozi)
                    {
                        product.ProductTotal -= 1;
                        _ctx.Products.Update(product);
                    }
                    
                    
                    _ctx.SaveChanges();
                }
                else
                {


                    var details = _ctx.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.ProductId == id);

                    if (details == null)
                    {
                        _ctx.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            Count = 1,
                            Price = (product.DiscountId.StartDate < DateTime.Now && product.DiscountId.EndtDate > DateTime.Now) ? priceProduct - (product.DiscountId.DiscountPercent * priceProduct) / 100 : priceProduct,
                            ProductId = id
                            
                        });

                    }
                    else
                    {

                        details.Count += 1;
                        _ctx.Update(details);
                    }



                    if (!tozi)
                    {
                        product.ProductTotal -= 1;
                        _ctx.Products.Update(product);
                    }
                    



                    _ctx.SaveChanges();

                }
                UpdateSumOrder(order.OrderId);
            }
            return Redirect("/");
        }
        public async Task<IActionResult> ShowOrder()
        {
            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            tozi = ((ClaimsIdentity)User.Identity).Claims
              .Where(c => c.Type == ClaimTypes.Role)
              .Select(c => c.Value).Contains("توزیع کننده");
            
            Order order = _ctx.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinaly);
            
            OrderDetail orderDetail;
            List<ShowOrderViewModel> _list = new List<ShowOrderViewModel>();
            if (order != null)
            {
                
                var details = _ctx.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                ViewBag.tozi = order.Marhale;
                foreach (var item in details)
                {
                    var product = await _ctx.Products.Include(x=>x.DiscountId).Where(x=>x.ProductID == item.ProductId).FirstOrDefaultAsync();
                    int priceProduct = (tozi) ? product.PriceToziKonande : product.Price;
                    if ((product.DiscountId.StartDate < DateTime.Now && product.DiscountId.EndtDate > DateTime.Now) == false)
                    {
                        orderDetail = await _ctx.OrderDetails.FindAsync(item.OrderDetailID);
                        orderDetail.Price = priceProduct;
                        _ctx.OrderDetails.Update(orderDetail);
                        await _ctx.SaveChangesAsync();
                    }
                    else {
                        orderDetail = await _ctx.OrderDetails.FindAsync(item.OrderDetailID);
                        orderDetail.Price = priceProduct - (product.DiscountId.DiscountPercent * priceProduct) / 100;
                        _ctx.OrderDetails.Update(orderDetail);
                        await _ctx.SaveChangesAsync();
                    }
                    
                    _list.Add(new ShowOrderViewModel()
                    {
                        Count = item.Count,
                       // ImageName = product.ImageName,

                        OrderDetailId = item.OrderDetailID,
                        Price = item.Price,
                        Sum = item.Count * item.Price,
                        Title = product.ProductName
                    });

                }
            }

            return View(_list);
        }
        //public IActionResult Delete(int id)
        //{
        //    var orderDetail = _ctx.OrderDetails.Find(id);
        //    _ctx.Remove(orderDetail);
        //    _ctx.SaveChanges();
        //    return RedirectToAction("ShowOrder");
        //}

        public IActionResult Command(int id, string command)
        {
            var orderDetail = _ctx.OrderDetails.Find(id);
            var product = _ctx.Products.Find(orderDetail.ProductId);
            var order = _ctx.Orders.Find(orderDetail.OrderId);
            tozi = ((ClaimsIdentity)User.Identity).Claims
              .Where(c => c.Type == ClaimTypes.Role)
              .Select(c => c.Value).Contains("توزیع کننده");
            switch (command)
            {
                case "up":
                    {
                        if (product.ProductTotal>30 || tozi)
                        {
                            
                            
                            orderDetail.Count += 1;
                            

                            _ctx.Update(orderDetail);
                            if (!tozi)
                            {
                                product.ProductTotal -= 1;
                                _ctx.Products.Update(product);
                            }
                            
                            
                            
                        }
                        break;
                    }
                case "down":
                    {
                        orderDetail.Count -= 1;
                        if (!tozi)
                        {
                            product.ProductTotal += 1;
                            _ctx.Products.Update(product);
                        }
                        
                        if (orderDetail.Count == 0)
                        {
                           
                            _ctx.OrderDetails.Remove(orderDetail);
                        //    _ctx.Orders.Remove(order);
                            
                        }
                        else
                        {
                            _ctx.Update(orderDetail);
                            
                        }
                        
                        break;
                    }
                case "delete":
                    {
                        if (!tozi)
                        {
                            product.ProductTotal += orderDetail.Count;
                            _ctx.Products.Update(product);
                        }
                        
                        _ctx.OrderDetails.Remove(orderDetail);
                        
                        break;
                    }
            }


            _ctx.SaveChanges();
            UpdateSumOrder(order.OrderId);
            return RedirectToAction("ShowOrder");
            
        }

        public IActionResult PaymentOne(string address,string radiobutton) {

            return radiobutton switch
            {
                "option1" => RedirectToAction("PaymentCartByEtbar", new { address = address }),
                "option2" => RedirectToAction("Payment", new
                {
                    address = address
                }),
                _ => NotFound(),
            };
        }


        public async Task<IActionResult> PaymentCartByEtbar(string address)
        {
            if (address == null)
            {
                return Content("خطا آدرس وارد شده صحیح نیست");
            }

            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _ctx.Orders.SingleOrDefault(o => !o.IsFinaly && o.UserId == CurrentUserID);
            if (order == null || order.IsFinaly)
            {
                return NotFound();
            }

            if (_userService.BalanceUserWallet(order.UserId) >= order.Sum)
            {
                order.IsFinaly = true;
                order.Address = address;

                _userService.AddWallet(new Wallet
                {
                    Amount = order.Sum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    Description = "شماره فاکتور شما" + order.OrderId,
                    UserId = CurrentUserID,
                    TypeId = 2
                });
                _ctx.Orders.Update(order);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("FactorUser", "userpanel");
            }
            return Content("شارژ اعتبار شما کافی نیست ");



        }
        public IActionResult Payment(string address)
        {
            if (address == null)
            {
                return Content("خطا آدرس وارد شده صحیح نیست");
            }
            string CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _ctx.Orders.SingleOrDefault(o => !o.IsFinaly && o.UserId == CurrentUserID);
            if (order == null || order.IsFinaly)
            {
                return NotFound();
            }

            var payment = new Payment(order.Sum);
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "https://localhost:44394/Home/OnlinePayment/" + order.OrderId);
            
            
            if (res.Result.Status == 100)
            {
                order.Address = address;
                _ctx.Update(order);
                _ctx.SaveChanges();
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }

   
        }

        public void UpdateSumOrder(int orderId)
        {
            var order = _ctx.Orders.Find(orderId);
            if (order !=null)
            {
                order.Sum = _ctx.OrderDetails.Where(o => o.OrderId == order.OrderId).Select(d => d.Count * d.Price).Sum();
                order.QTY = _ctx.OrderDetails.Where(o => o.OrderId == order.OrderId).Sum(x => x.Count);
                _ctx.Update(order);
                _ctx.SaveChanges();
            }
            
        }
    }
}