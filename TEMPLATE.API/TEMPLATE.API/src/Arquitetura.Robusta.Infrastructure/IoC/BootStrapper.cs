using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces;
using Template.Application.Services;
using Template.Shared.Kernel.Domain.Notifications;
using Template.Infrastructure.Data;
using Template.Infrastructure.Data.Context;
using Template.Infrastructure.Data.Repository;
using Template.Infrastructure.Notifications;
using Template.Shared.Kernel.Application;
using Template.Shared.Kernel.Data;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Domain.Handlers;
using Template.Shared.Kernel.Mediator;
using Template.Shared.Kernel.Util;
using Template.Domain.Aggregates.Clientes.Events;
using Template.Domain.Aggregates.Clientes.Handlers;
using Template.Domain.Aggregates.Clientes.Services;
using Template.Domain.Aggregates.Clientes.Repository;
using Template.Application.Services.External;

namespace Template.Infrastructure.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IMultipartFormDataService, MultipartFormDataService>();
            services.AddScoped<IClienteAppService, ClienteAppService>();
            services.AddScoped<IClienteExternalAppService, ClienteExternalAppService>();

            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Events            
            services.AddScoped<INotificationHandler<DomainNotificationEvent>, DomainNotificationEventHandler>();
            services.AddScoped<INotificationHandler<ClienteExcluidoEvent>, ClienteExcluidoEventHandler>();


            // Domain
            services.AddScoped<IClienteService, ClienteService>();

            // Data
            services.AddScoped<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteRepository, ClienteRepository>();


            // Util
            services.AddScoped<ICryptographyUtil, CryptographyUtil>();

            // Notifications
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ViewRenderService>();
        }
    }
}
