using MediatR;
using Template.Shared.Kernel.Data;
using Template.Shared.Kernel.Domain;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Domain.Handlers;
using Template.Shared.Kernel.Mediator;
using System.Threading.Tasks;

namespace Template.Shared.Kernel.Application
{
    public class ApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        protected readonly IMediatorHandler _mediator;
        protected readonly DomainNotificationEventHandler _notifications;

        public ApplicationService(IUnitOfWork unitOfWork,
                                  IMediatorHandler mediator,
                                  INotificationHandler<DomainNotificationEvent> notifications)
        {
            _unitOfWork = unitOfWork;
            _notifications = (DomainNotificationEventHandler)notifications;
            _mediator = mediator;
        }       

        protected async Task<bool> Commit()
        {
            if (!IsValidOperation()) 
                return false;

            if (await _unitOfWork.CommitAsync()) 
                return true;

            RaiseError("Erro ao persistir dados.");
            return false;
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasError());
        }

        protected void RaiseError(string message)
        {
            _mediator.PublishEvent(new DomainNotificationEvent(DomainNotificationType.Error, GetType().Name, message));
        }

        protected void RaiseInformation(string message)
        {
            _mediator.PublishEvent(new DomainNotificationEvent(DomainNotificationType.Information, GetType().Name, message));
        }
    }
}
