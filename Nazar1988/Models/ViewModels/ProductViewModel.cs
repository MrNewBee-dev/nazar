using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(IEnumerable<TreeViewCategory> viewCategories)
        {
            Categories = viewCategories;
        }


        public ProductViewModel()
        {

        }

        public IEnumerable<TreeViewCategory> Categories { get; set; }

        
        [Display(Name ="نامحصول")]
        public string ProductName { get; set; }
        [Display(Name = "کد محصول")]
        public int ProductNumber { get; set; }//code mahsol
        [Display(Name = "تعداد محصول")]
        public int ProductTotal { get; set; }//tedad kole mahsolat
        [Display(Name = "تعداد فروخته شده ")]
        public int? ProductSold { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime? CreateDate { get; set; }//roze ijad mahsool
        [Display(Name = "منتشر شده")]
        public bool IsPublish { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "قیمت برای توزیع کننده")]
        public int PricetoziKonande { get; set; }


        [Display(Name = "شرح محصول")]
        public string ProductDescription { get; set; }

        public string ProductUrl { get; set; }

        [Display(Name = "نوع دسته ")]
        public int[] CategoryId { get; set; }
        [Display(Name = "تخفیفات")]
        public int DiscountId { get; set; }
        [Display(Name = "عکس محصول")]
        public string Image { get; set; }
        
    }
    public class ProductViewModelViews {

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int priceTozi { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductEditViewModelViews
    {

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        
        
        
        public int ProductNumber { get; set; }//code mahsol
        
        public int ProductTotal { get; set; }//tedad kole mahsolat
        
        
        
        public bool IsPublish { get; set; }
        
        public int Price { get; set; }

        
        public int PricetoziKonande { get; set; }

        public int ProductId { get; set; }
    }
}
