using Services.Interfaces;
using Services.Services;

namespace ChanV3.Startup
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IThreadService, ThreadService>();
            return services;
        }
    }
}
