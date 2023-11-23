using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class ChargeWalletViewModel
    {
        [Display(Name ="مبلغ")]
        [Required(ErrorMessage ="وارد کردین این فیلد الزامی است")]
        public int Amount { get; set; }
    }
    public class WalletViewModel
    {
        
        public int Amount { get; set; }
        
        public int Type { get; set; }
        
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

    }

    public class BedehiViewModel
    {

        public int Amount { get; set; }

        public string Email { get; set; }
        public string UserId { get; set; }
       




    }

    public class WalletViewModelAdmin
    {
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "وارد کردین این فیلد الزامی است")]
        public int Amount { get; set; }
        [Display(Name = "نوع پرداخت")]
        [Required(ErrorMessage = "وارد کردین این فیلد الزامی است")]
        public int Type { get; set; }
        [Display(Name = "بابت")]
        [Required(ErrorMessage = "وارد کردین این فیلد الزامی است")]
        public string Description { get; set; }
        [Display(Name = "شماره چک")]
        [Required(ErrorMessage = "وارد کردین این فیلد الزامی است")]
        public string CheckId { get; set; }
        public DateTime DateTime { get; set; }
        [Display(Name = "پرداخت حضوری")]
        public bool PardakhtOni { get; set; }
    }
}
