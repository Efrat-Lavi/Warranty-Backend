using Warranty.Core.Interfaces.Repositories;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Interfaces;
using Warranty.Data.Repositories;
using Warranty.Data;
using Warranty.Service;
using Warranty.Core;
using Recipes.Core.Interfaces.IServices;
using Recipes.Service.Services;

namespace Warranty.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile), typeof(MappingPostProfile));

            //repositories

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWarrantyRepository, WarrantyRepository>();

            //services
            services.AddScoped<ICompanyServices, CompanyServices>();
            services.AddScoped<IPermissionServices, PermissionServices>();
            services.AddScoped<IRecordServices, RecordServices>();
            services.AddScoped<IRoleServices, RoleServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWarrantyServices, WarrantyServices>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IS3Service, S3Service>();
            services.AddScoped<IEmailService, EmailService>();

            //Email



            // הוספת ה-Service
            services.AddHostedService<WarrantyExpirationJob>();





        }
    }
}
