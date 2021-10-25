using JWTMicroNetCore.Data;
using JWTMicroNetCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTMicroNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();
            services
              .AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = true;
                   cfg.SaveToken = true;
                   cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JWTSecretKey"))),
                       //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("placeholder-key-that-is-long-enough-for-sha256")),
                       ValidateAudience = false,
                       ValidateIssuer = false,
                       ValidateLifetime = false,
                       RequireExpirationTime = false,
                       ClockSkew = TimeSpan.Zero,
                       ValidateIssuerSigningKey = true
                   };
               });

            services.AddScoped<ITokenBuilder, TokenBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
