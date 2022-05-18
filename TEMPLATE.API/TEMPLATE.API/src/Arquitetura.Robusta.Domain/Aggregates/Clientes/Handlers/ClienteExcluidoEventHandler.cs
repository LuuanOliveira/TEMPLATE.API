using MediatR;
using Template.Domain.Aggregates.Clientes.Events;
using Template.Shared.Kernel.Domain.Notifications;
using Template.Shared.Kernel.GuardCauses;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Domain.Aggregates.Clientes.Handlers
{
    public class ClienteExcluidoEventHandler : INotificationHandler<ClienteExcluidoEvent>
    {
        private readonly IEmailSender _emailSender;

        public ClienteExcluidoEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(ClienteExcluidoEvent notification, CancellationToken cancellationToken)
        {
            Guard.Null(notification, nameof(ClienteExcluidoEvent));

            await _emailSender.SendMailAsync(notification.ClienteExcluido.Email.Endereco, "TESTE", "<h1>TESTE</>");
        }
    }
}
