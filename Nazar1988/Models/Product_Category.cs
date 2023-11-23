using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class Product_Category
    {
      
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public Product Product{ get; set; }
        public Category Category { get; set; }

    }
}
