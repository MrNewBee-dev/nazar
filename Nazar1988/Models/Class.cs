using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class ProductsDiscountModel
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int ProductTotal { get; set; }
        public bool IsPublish{ get; set; }
        public int PriceToziKonande { get; set; }
        public string ProductDescription { get; set; }
        public string ImagePath  { get; set; }
        public byte DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndtDate { get; set; }

    }
}
