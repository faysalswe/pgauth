 using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenAuthWithPG.Data;
using TokenAuthWithPG.Model;

namespace TokenAuthWithPG.Controllers
{
    [Route("api/[controller]")] 
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if(user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
               

                //var claims = new[]
                //{
                //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //};

                
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"));
                var token = new JwtSecurityToken(
                    issuer:"faysal",
                    audience: "audience",
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                });
            }
            return Unauthorized();
        }

    }
}