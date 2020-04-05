using System;
using System.Text;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models.AppSettings;
using BLL.Models.Email;
using BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApi.Extensions;
using Hangfire;
using Hangfire.SqlServer;
using FluentValidation.AspNetCore;

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

            var emailConfigSection = Configuration.GetSection("EmailConfiguration");
            services.Configure<EmailConfiguration>(emailConfigSection);

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
                                           ValidateAudience = false,
                                           ValidateIssuerSigningKey = true,
                                           ValidIssuer = appSettings.JwtIssuer,
                                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtKey))
                                       };
                                   }
                     );

            services.ConfigureCors();

            // inject httpcontext in services
            services.AddHttpContextAccessor();

            // hangfire configuration
            services.AddHangfire(config => config
                                                 .UseSqlServerStorage(Configuration.GetConnectionString("homeConnection"), new SqlServerStorageOptions
                                                 {
                                                     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                                     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                                     QueuePollInterval = TimeSpan.Zero,
                                                     UseRecommendedIsolationLevel = true,
                                                     UsePageLocksOnDequeue = true,
                                                     DisableGlobalLocks = true
                                                 }));

            // configure validators
            services.AddMvc()
                .AddFluentValidation();
            services.ConfigureValidators();

            // Dependency Injection
            services.AddScoped<IStudentProfileService, StudentProfileService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailService, MailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");


            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            
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
