using Blitz.Application.Interfaces;
using Blitz.Application.Services;
using Blitz.Infrastructure.Interfaces;
using Blitz.Infrastructure.Repositories;

namespace Blitz.API.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<IServiceImage, ImageService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuthService, UserService>();
            services.AddScoped<IAuthentication, UserRepository>();
            services.AddScoped<IDocument, DocumentRepository>();
            services.AddScoped<ICode, CodeRepository>();
            services.AddScoped<IBorrower, BorrowerRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVideoService, VideoService>();

            return services;
        }
    }
}
