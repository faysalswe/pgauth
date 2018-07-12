using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthWithPG.Model
{
    public class Entry
    {
        [key]
        public int EntryID { get; set; }
        public int VisitorID { get; set; }
        public string Visitee { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
