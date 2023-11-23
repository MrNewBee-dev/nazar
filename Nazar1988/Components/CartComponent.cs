using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Nazar1988.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nazar1988.Models;
using Nazar1988.Areas.MyMaster.Controllers;
using System.IO;

namespace Cart_Exam.Components
{
    public class CartComponent:ViewComponent
    {
        private NazarDbContext _ctx;

        public CartComponent(NazarDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ShowCartViewModel> _list=new List<ShowCartViewModel>();

            if (User.Identity.IsAuthenticated)
            {
                string CurrentUserID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var order = _ctx.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinaly);
                if (order != null)
                {
                    var details = _ctx.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                    foreach (var item in details)
                    {
                        var product = _ctx.Products.Find(item.ProductId);
                        _list.Add(new ShowCartViewModel()
                        {
                            Count = item.Count,
                            Title = product.ProductName,
              //              ImageName = Path.GetFileName(System.Text.Json.JsonSerializer.Deserialize<List<string>>(product.ImagePath).First())
                    });

                    }
                }

            }

            return View("/Views/Shared/_ShowCart.cshtml", _list);
        }
    }
}
