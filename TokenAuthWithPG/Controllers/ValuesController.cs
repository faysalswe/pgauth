using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenAuthWithPG.Data;
using TokenAuthWithPG.Model;
namespace TokenAuthWithPG.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object Get()
        {
            List<Visitor> lstvisitor = new List<Visitor>
            {
                new Visitor{ VisitorID =8111,  Mobile = "1111199999", Name = "Faysal"},
                new Visitor{ VisitorID =8222,  Mobile = "3333377777", Name = "Abid"},
                new Visitor{ VisitorID =8333,  Mobile = "3333377777", Name = "Abid"}
            };

            List<Entry> lstentry = new List<Entry>
            {
                new Entry { EntryID = 1111, EntryTime= DateTime.Now , VisitorID = 8111, Visitee = "Einstine"},
                new Entry { EntryID = 2222, EntryTime= DateTime.Now , VisitorID = 8222, Visitee ="karl Sagan"},
                new Entry { EntryID = 3333, EntryTime= DateTime.Now , VisitorID = 8333, Visitee = "Remi Malek"},
                new Entry { EntryID = 4444, EntryTime= DateTime.Now , VisitorID = 8111, Visitee = "Enistine"},
                new Entry { EntryID = 5555, EntryTime= DateTime.Now , VisitorID = 8222, Visitee ="Karl Sagan"}
            };

            List<Exit> lstexit = new List<Exit>
            {
                new Exit{ ExitID =9111, EntryID= 1111 , ExitTime =DateTime.Now, ExitNote ="1111 id exited"  },
                new Exit{ ExitID =9222, EntryID= 2222 , ExitTime =DateTime.Now, ExitNote ="1111 id exited"  },
                new Exit{ ExitID =9333, EntryID= 3333 , ExitTime =DateTime.Now, ExitNote ="1111 id exited"  }
            };
            var result = from entry in lstentry
                         join visitor in lstvisitor
                         on entry.VisitorID equals visitor.VisitorID
                         join exit in lstexit on
                         entry.EntryID equals exit.EntryID
                         into ps
                         select new
                         {
                             entryId = entry.EntryID,
                             name = visitor.Name,
                             mobile = visitor.Mobile,
                             visitee = entry.Visitee,
                             entrytime = entry.EntryTime,
                             exit = ps
                         };

            return result;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IEnumerable<string> GetUser ()
        {
            return _context.Users.Select(u => u.UserName).ToArray();
        }
        
        [Authorize(Roles = "Visitor")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value :"+ id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}
