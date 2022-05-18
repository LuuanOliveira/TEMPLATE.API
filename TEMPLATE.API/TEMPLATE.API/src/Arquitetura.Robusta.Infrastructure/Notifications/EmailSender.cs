using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Template.Shared.Kernel.Domain.Notifications;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Drawing;
using System.Linq;

namespace Template.Infrastructure.Notifications
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly EmailConfig _emailConfig;
        private readonly ViewRenderService _viewRenderService;

        public EmailSender(IConfiguration config, ViewRenderService viewRenderService, ILogger<EmailSender> logger)
        {
            _emailConfig = _emailConfig = config.GetSection("Email").Get<EmailConfig>();
            _viewRenderService = viewRenderService;
            _logger = logger;
        }

        public Task SendMailAsync(string receiverEmail, string subject, string viewName, object model, List<EmailAttachmentModel> attachments = null)
        {
            return SendMailAsync(new List<string>() { receiverEmail }, subject, viewName, model, attachments);
        }

        public Task SendMailAsync(string receiverEmail, string subject, string body, List<EmailAttachmentModel> attachments = null)
        {
            return SendMailAsync(new List<string>() { receiverEmail }, subject, body, attachments);
        }

        public async Task SendMailAsync(List<string> receiversEmails, string subject, string viewName, object model, List<EmailAttachmentModel> attachments = null)
        {
            var body = await _viewRenderService.RenderToStringAsync(viewName, model);
            await SendMailAsync(receiversEmails, subject, body, attachments);
        }

        public async Task SendMailAsync(List<string> receiversEmails, string subject, string body, List<EmailAttachmentModel> attachments = null)
        {
            if (!_emailConfig.Active)
            {
                return;
            }

            try
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _emailConfig.SenderEmail,
                        Password = _emailConfig.SenderPassword,
                    };

                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Host = _emailConfig.Host;
                    client.Port = _emailConfig.Port.GetValueOrDefault();
                    client.EnableSsl = _emailConfig.EnableSSL.GetValueOrDefault();

                    var receivers = _emailConfig.SendOnlyTo == null || !_emailConfig.SendOnlyTo.Any()
                      ? receiversEmails
                      : _emailConfig.SendOnlyTo;

                    using (var emailMessage = new MailMessage())
                    {
                        receivers.ForEach(e => emailMessage.To.Add(new MailAddress(e)));

                        emailMessage.From = new MailAddress(_emailConfig.SenderEmail);
                        emailMessage.Subject = subject;
                        emailMessage.Body = body;
                        emailMessage.IsBodyHtml = true;

                        //AddImageToEmail(emailMessage, Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo-email.png")));

                        if (attachments != null)
                        {
                            SendMessageWithAttachments(client, emailMessage, attachments);
                        }
                        else
                        {
                            SendMessage(client, emailMessage);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            await Task.CompletedTask;
        }

        private void SendMessage(SmtpClient client, MailMessage emailMessage)
        {
            client.Send(emailMessage);
        }

        private void SendMessageWithAttachments(SmtpClient client, MailMessage emailMessage, List<EmailAttachmentModel> attachments)
        {
            List<MemoryStream> memoryStreams = new List<MemoryStream>();
            try
            {
                attachments.ForEach(a =>
                {
                    var memoryStream = new MemoryStream(a.Content);
                    memoryStreams.Add(memoryStream);
                    emailMessage.Attachments.Add(new Attachment(memoryStream, a.Name));
                });

                SendMessage(client, emailMessage);
            }
            finally
            {
                memoryStreams.ForEach(m => m.Dispose());
            }
        }

        private static void AddImageToEmail(MailMessage mail, Image image)
        {
            var imageStream = GetImageStream(image);

            var imageResource = new LinkedResource(imageStream, "image/png") { ContentId = "logo-email.png" };
            var alternateView = AlternateView.CreateAlternateViewFromString(mail.Body, mail.BodyEncoding, MediaTypeNames.Text.Html);

            alternateView.LinkedResources.Add(imageResource);
            mail.AlternateViews.Add(alternateView);
        }

        private static Stream GetImageStream(Image image)
        {
            var imageConverter = new ImageConverter();
            var imgaBytes = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            var memoryStream = new MemoryStream(imgaBytes);

            return memoryStream;
        }
    }
}
