using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Shared.Kernel.Domain.Notifications
{
    public interface IEmailSender
    {
        Task SendMailAsync(string receiverEmail, string subject, string viewName, object model, List<EmailAttachmentModel> attachments = null);
        Task SendMailAsync(string receiverEmail, string subject, string body, List<EmailAttachmentModel> attachments = null);
        Task SendMailAsync(List<string> receiversEmails, string subject, string viewName, object model, List<EmailAttachmentModel> attachments = null);

    }
}
