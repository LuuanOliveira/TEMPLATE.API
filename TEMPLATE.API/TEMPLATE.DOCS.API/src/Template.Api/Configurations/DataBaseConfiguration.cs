using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Shared.Kernel.GuardCauses;

namespace Template.Api.Configurations
{
    public static class DataBaseConfiguration
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Guard.Null(services, nameof(services));
            Guard.Null(configuration, nameof(configuration));

            services.AddDbContext<Template.Api.Data.Context.DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }
    }
}
