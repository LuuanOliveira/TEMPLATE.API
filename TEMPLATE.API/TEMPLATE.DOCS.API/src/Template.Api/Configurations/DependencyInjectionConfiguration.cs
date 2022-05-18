using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.Shared.Kernel.Data;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Domain.Handlers;
using Template.Shared.Kernel.Mediator;
using Template.Api.Infrastructure.Data;
using Template.Application.AutoMapper;

namespace Template.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
            services.AddAutoMapper(typeof(ViewModelToDomainMappingProfile));

            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Events            
            services.AddScoped<INotificationHandler<DomainNotificationEvent>, DomainNotificationEventHandler>();

            // Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
