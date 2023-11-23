using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Areas.Identity.Data
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationRole Role { get; set; }

        public virtual Nazar1988User User { get; set; }
    }
}
