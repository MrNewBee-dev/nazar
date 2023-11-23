using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Nazar1988.Models;
using Quartz;

namespace Nazar1988
{
    [DisallowConcurrentExecution]
    public class RemoveCartJob : IJob
    {
       
        public Task Execute(IJobExecutionContext context)
        {
           var option = new DbContextOptionsBuilder<NazarDbContext>();
            option.UseSqlServer(@"Server=.;Database=Nazar1988Product;Trusted_Connection=True;MultipleActiveResultSets=true");
           
            using (NazarDbContext _ctx =new NazarDbContext(option.Options))
            {
                var orders = _ctx.Orders.Where(o => !o.IsFinaly && o.CreateDate < DateTime.Now.AddHours(-24)).ToList();
                foreach (var order in orders)
                {
                       var orderDetail = _ctx.OrderDetails.Where(od => od.OrderId == order.OrderId).ToList();
                    
                    foreach (var detail in orderDetail)
                    {
                        var Product = _ctx.Products.Find(detail.ProductId);
                        Product.ProductTotal += detail.Count;
                        _ctx.Products.Update(Product);
                    }
                    _ctx.Remove(order);
                }

                _ctx.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
