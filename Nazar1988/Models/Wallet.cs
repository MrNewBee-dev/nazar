using Nazar1988.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class Wallet
    {
        public Wallet()
        {

        }
        [Key]
        public int WalletId { get; set; }


        [ForeignKey("WalleTTypes")]
        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }

        
        public bool Etebar { get; set; }


        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }


        public string Shomare_Check { get; set; }


        public virtual Nazar1988User User { set; get; }
        public virtual WalleTType WalleTTypes { set; get; }


    }
}
