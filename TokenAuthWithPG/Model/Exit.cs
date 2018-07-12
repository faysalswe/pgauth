using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthWithPG.Model
{
    public class Exit
    {
        [key]
        public int ExitID { get; set; }
        public DateTime ExitTime { get; set; }
        public string ExitNote { get; set; }
        public int EntryID { get; set; }
    }
}
