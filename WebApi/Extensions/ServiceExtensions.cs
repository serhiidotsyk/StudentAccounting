using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BLL.Models.Auth;
using BLL.Helpers.FluentValidation.Auth;
using BLL.Helpers.FluentValidation.Course;
using BLL.Models.Course;
using BLL.Models.UserCourseModel;
using BLL.Models.StudentProfile;
using BLL.Helpers.FluentValidation.User;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("homeConnection"), b =>
                    b.MigrationsAssembly("DAL")));

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                //s.OperationFilter<AddAuthHeaderParam.AddAuthHeaderParam>();
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StudentAccounting", Version = "v1" });
            });
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                             .AllowAnyHeader());
            });
        public static void ConfigureValidators(this IServiceCollection services)
        {
            // Auth Fluent Validation
            services.AddTransient<IValidator<UserSocialLogin>, UserSocialLoginValidation>();
            services.AddTransient<IValidator<UserSignInModel>, SignInValidation>();
            services.AddTransient<IValidator<UserSignUpModel>, SignUpValidation>();

            // Course Fluent Validation
            services.AddTransient<IValidator<UserCourseModel>, UserCourseValidation>();
            services.AddTransient<IValidator<SubscribeToCourseModel>, SubscribeToCourseValidation>();

            // User Fluent Validation
            services.AddTransient<IValidator<UserModel>, UserValidation>();
        }
    }
}
