using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nazar1988.Models
{
    public class Country
    {
        public Country()
        {
            Cities=new List<City>();
        }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual IList<City> Cities { get; set; }
    }
}