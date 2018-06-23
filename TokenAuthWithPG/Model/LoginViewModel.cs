using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthWithPG.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
