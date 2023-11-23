using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Nazar1988.Models;

namespace Nazar1988.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Nazar1988User class
    public class Nazar1988User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
       
        public DateTime RegisterDate { get; set; }
        public DateTime LastVisitDateTime { get; set; }
       
        public int KifePool { set; get; }
       // public virtual ICollection<Order> Orders { get; set; }
        public virtual List<ApplicationUserRole> Roles { get; set; }
        public virtual List<Wallet> Wallets { get; set; }
        


    }
}
