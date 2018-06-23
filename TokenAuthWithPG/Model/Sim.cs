using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthWithPG.Model
{
    public class Sim
    {
        [Key]
        public string Number { get; set; }
        public string Operator { get; set; }
        public bool Status { get; set; }
        public string UserID { get; set; }
    }
}
