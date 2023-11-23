using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class AccounterModel
    {
        
        public DateTime CreateDate { get; set; }

        
        public int Sum { get; set; }
        public int OrderId { get; set; }
        public byte Marhale { get; set; }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int QTY { get; set; }
        public string Address { get; set; }


    }
}
