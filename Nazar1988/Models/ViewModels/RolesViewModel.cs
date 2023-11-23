using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models.ViewModels
{
    
        public class RolesViewModel
        {
            public string RoleID { get; set; }

            [Display(Name = "عنوان نقش")]
            [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
            public string RoleName { get; set; }

            
        }
    
}
