using BangOnERP.DataBase;
using MasterLocation.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using System.Text;

namespace MasterCountry
{
    public class Startup
    {
        public const string SECRET = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE  IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            string sqlconn = "Server=DESKTOP-HHH3QT6\\SQLEXPRESS;Database=BangOnERP;Trusted_Connection=True;";
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                 .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddControllers();
            services.AddDbContext<BangOnERPContext>(options => options.UseSqlServer(sqlconn));
            services.AddTransient<ICountry, CountryRepository>();
            services.AddTransient<IState, StateRepository>();
            services.AddTransient<ICity, CityRepository>();
            var key = Encoding.ASCII.GetBytes(SECRET);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
