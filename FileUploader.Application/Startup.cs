using FileUploader.Application.Handlers.FileUploadHandler;
using FileUploader.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploader.Application
{
    public static class Startup
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(FileUploadHandler));
            services.AddTransient(typeof(GetFileHandler));
        }
    }
}
