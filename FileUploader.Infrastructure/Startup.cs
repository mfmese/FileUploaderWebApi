using FileUploader.Domain.Ports;
using FileUploader.Infrastructure.Contexts;
using FileUploader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploader.Infrastructure
{
    public static class Startup
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");

            services.AddDbContext<FileUploadDataContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Transient);

            services.AddTransient<IFileUploadPort, FileUploadRepository>();
            services.AddTransient<ILogPort, LogRepository>();
        }
    }
}
