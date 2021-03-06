﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CoursesP2P.App.Areas.Identity.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private SendGridOptions options;
        public SendGridEmailSender(IOptions<SendGridOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.options.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("coursesp2p@gmail.com", "CoursesP2P");
            var to = new EmailAddress(email, email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            var body = await response.Body.ReadAsStringAsync();
        }
    }
}
