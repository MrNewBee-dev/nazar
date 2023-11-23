using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nazar1988.Models
{
    public class WalleTType
    {
        public WalleTType()
        {

        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public  int TypeId { get; set; }
        [Required]
        [MaxLength(20)]
        public string TypeTitle { get; set; } //onvane code ke bar migardanad agar 1 bood bedehkar 2 bood pestankar
        public virtual List<Wallet> Wallets { get; set; } 
    }
}
