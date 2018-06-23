using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthWithPG.Data
{
    public class SeedDatabase
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            context.Database.EnsureCreated();

            if (!context.Users.Any() || !context.Roles.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "faysal4ah@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "faysal"
                };

                ApplicationUser user1 = new ApplicationUser()
                {
                    Email = "abid@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "abid"
                };

                IdentityRole role = new IdentityRole()
                { 
                    Name = "Admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                IdentityRole role1 = new IdentityRole()
                {
                    Name = "Visitor",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                await userManger.CreateAsync(user, "SuperSecret!2");
                await userManger.CreateAsync(user1, "SuperSecret!2");
                await roleManger.CreateAsync(role);
                await roleManger.CreateAsync(role1);

                await userManger.AddToRoleAsync(await userManger.FindByNameAsync("faysal"), "admin");
                await userManger.AddToRoleAsync(await userManger.FindByNameAsync("abid"), "visitor");
            }
        }
    }
}
