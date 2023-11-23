using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class Discount
    {
        [Key,ForeignKey("Product")]
        public int ProductId { get; set; }
        public byte DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndtDate { get; set; }

        public Product ProductIdies { get; set; }
    }
}
