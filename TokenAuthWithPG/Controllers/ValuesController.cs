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
        public IEnumerable<string> Get()
        {
            return new string[]{ "El Chapo","Escobar"};
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IEnumerable<string> GetUser ()
        {
            return _context.Users.Select(u => u.UserName).ToArray();
            //return new string[]{ "El Chapo","Escobar"};
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
