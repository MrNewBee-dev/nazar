using Microsoft.EntityFrameworkCore.Storage.Internal;
using Nazar1988.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required]
        [Column(TypeName = "nvarchar(450)")]
        [ForeignKey("User")] 
        public string UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int Sum { get; set; }

        public bool IsFinaly { get; set; }

        public byte Marhale { set; get; } /// <summary>
        ///  agar 0 boood erja hesabdari
        ///  agar 1 boood erja anbardari
        ///  agar 2 boood ersall shode
        /// </summary>
        public List<OrderDetail> OrderDetails { get; set; }
        [MaxLength(50, ErrorMessage = "بیشتر از حد مجاز است")]
        
        public string Address { set; get; }

        public int QTY { set; get; }
        //public virtual Nazar1988User ApplicationUser { get; set; }


    }
}
