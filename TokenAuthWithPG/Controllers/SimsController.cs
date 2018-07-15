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


        public void Test1()
        {
            string Test1 = "Test 1";
        }

        public void Test()
        {
            string test = "Test";
        }

        public void Test12()
        {
            string Test1 = "Test 1";
        }

        public void Test13()
        {
            string test = "Test 3";
        }

        public void Test14()
        {
            string Test1 = "Test 4";
        }

        public void Test15()
        {
            string test = "Test";
        }

        public void Test16()
        {
            string Test1 = "Test 1";
        }

        public void Test17()
        {
            string test = "Test";
        }
    }
}