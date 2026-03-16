using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Constracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.Management.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSetting _emailSetting { get; }
        public EmailSender(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }

        public async Task<bool> SendEmail(EmailMessage email)
        {
            var client = new SendGridClient(_emailSetting.Apikey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);

            //return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
            return response.IsSuccessStatusCode;
        }
    }
}
