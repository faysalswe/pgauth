using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokenAuthWithPG.Data;
using TokenAuthWithPG.Model;

namespace TokenAuthWithPG.Controllers
{
    [Produces("application/json")]
    [Route("api/Sims")]
    public class SimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // comment one 
        // comment two

        //comment three
        //comment four

        // comment five
        // comment six

        //comment seven
        //comment eight

        // comment nine
        // comment ten
    }
}