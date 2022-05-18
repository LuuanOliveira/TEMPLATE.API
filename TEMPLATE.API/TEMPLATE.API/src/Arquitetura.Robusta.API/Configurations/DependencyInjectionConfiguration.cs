using Microsoft.Extensions.DependencyInjection;
using Template.Application.AutoMapper;
using Template.Infrastructure.IoC;
using Template.Shared.Kernel.GuardCauses;

namespace Template.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            Guard.Null(services, nameof(services));

            BootStrapper.RegisterServices(services);

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
            services.AddAutoMapper(typeof(ViewModelToDomainMappingProfile));
        }
    }
}
