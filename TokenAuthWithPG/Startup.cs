using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TokenAuthWithPG.Data;
using TokenAuthWithPG.Model;

namespace TokenAuthWithPG
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }
        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }
        
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "faysal",
                    ValidateAudience = true,
                    ValidAudience = "audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"))
                    
                };
            });


            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(jwtBearerOptions =>
            //   {
            //       jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            //       {
            //           ValidateActor = true,
            //           ValidateAudience = true,
            //           ValidateLifetime = true,
            //           ValidateIssuerSigningKey = true,
            //           ValidIssuer = "Issuer",
            //           ValidAudience = "Audience",
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("signincredenttials"))
            //       };
            //   });

            //services.AddDbContext<PostgreSqlDbContext>(options => options.UseNpgsql("User ID=postgres;Password=secret;Host=localhost;Port=5432;Database=tawpg;Pooling=true;"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SeedDatabase.InitializeAsync(app.ApplicationServices.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);

            app.UseAuthentication();
            
            app.UseMvc();
        }
    }
}
