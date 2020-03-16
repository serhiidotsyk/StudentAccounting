using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.AppSettings;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApi.Extensions;

namespace WebApi
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
            services.AddControllers();

            services.ConfigureSqlContext(Configuration);

            // adding AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // adding swagger
            services.ConfigureSwagger();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            // configure jwt authentication 
            services.AddAuthentication(options =>
                                        {
                                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                        })
                    .AddJwtBearer(options =>
                                   {
                                       options.TokenValidationParameters = new TokenValidationParameters
                                       {
                                           ValidateIssuer = true,
                                           ValidateLifetime = true,
                                           ValidateIssuerSigningKey = true,
                                           ValidIssuer = appSettings.JwtIssuer,
                                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtKey))
                                       };
                                   }
                     );

            // Dependency Injection
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
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
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Accounting API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
