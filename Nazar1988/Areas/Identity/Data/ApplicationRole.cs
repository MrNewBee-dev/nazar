using Nazar1988.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Areas.Identity.Data
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
                
        }

        public ApplicationRole(string name) : base(name)
        {
                
        }

    

        public virtual List<ApplicationUserRole> Users { get; set; }
    }
}
