using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    public class UsersViewModel
    {
            public string Id { get; set; }

            [Display(Name = "نام کاربری")]
            public string UserName { get; set; }

            [Display(Name = "ایمیل")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
            public string Email { get; set; }

            [Display(Name = "شماره موبایل")]
            public string PhoneNumber { get; set; }

            [Display(Name = "نام")]
            public string FirstName { get; set; }

            [Display(Name = "نام خانوادگی")]
            public string LastName { get; set; }

            [Display(Name = "تاریخ عضویت")]
            public DateTime RegisterDate { get; set; }

            [Display(Name = "آخرین بازدید")]
            public DateTime? LastVisitDateTime { get; set; }
        
            [Display(Name = "کیف پول")]
            public int KifePool { get; set; }


            [Display(Name = "نقش ها")]
            public IEnumerable<string> Roles { get; set; }

            //public bool PhoneNumberConfirmed { get; set; }

            //public bool TwoFactorEnabled { get; set; }

            //public bool LockoutEnabled { get; set; }

            //public bool EmailConfirmed { get; set; }

            //public int AccessFailedCount { get; set; }

            //public DateTimeOffset? LockoutEnd { get; set; }

        
    }
    public class EditProfileViewModel
    {

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstName { get; set; }
        
        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }
        


    }
    public class EditProfileManagerViewModel
    {

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string FirstName { get; set; }
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Id { get; set; }

        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string LastName { get; set; }

        
        [Display(Name = "کیف پول")]
        public int KifePool { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }
        [Display(Name = "نقش ها")]
        public IEnumerable<string> Roles { get; set; }


    }
}
