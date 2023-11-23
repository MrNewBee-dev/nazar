using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class ProductDiscountViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int PriceToziKonande { get; set; }
        public string ProductDescription { get; set; }
        public string ImagePath { get; set; }
        public byte DiscountPercent { get; set; }

    }
}
