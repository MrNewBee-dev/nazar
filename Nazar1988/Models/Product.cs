using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductNumber { get; set; }//code mahsol
        public int ProductTotal { get; set; }//tedad kole mahsolat
        public int? ProductSold { get; set; }
        public DateTime? CreateDate { get; set; }//roze ijad mahsool
        public bool IsPublish { get; set; }
        public int Price { get; set; }
        public int PriceToziKonande { get; set; }
        public string ProductDescription { get; set; }

        public string ProductUrl { get; set; }
                        
        public Discount DiscountId { get; set; }
        
        public string ImagePath { get; set; }

        public List<Product_Category> book_Categories { get; set; }
        public List<OrderDetail> OrderDetails{ get; set; }
    }
}
