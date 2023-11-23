using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class ShowOrderViewModel
    {
        public int OrderDetailId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string Address { set; get; }
        public int Price { get; set; }
        public int Sum { get; set; }
        public int QTY { get; set; }

    }
    public class ShowOrderStimulViewModel
    {
        
        public int OrderId { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string Address { set; get; }
        public int Price { get; set; }
        public int Sum { get; set; }
        public int QTY { get; set; }
        public string Name { get; set; }

    }
}
