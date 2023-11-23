using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class AccounterViewModels
    {
        [Display(Name = "تاریخ صدور")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "مجموع قیمت")]
        public int SumPrice { get; set; }

        [Display(Name = "تایید حسابداری")]
        public int Marhale { get; set; }
     
        public string id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }


    }
}
