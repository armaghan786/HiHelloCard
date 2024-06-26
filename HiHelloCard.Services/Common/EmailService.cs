using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.ViewModel.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Services.Common
{
    public class EmailService : IEmailService
    {
        private const string templatepath = "EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        public EmailService(IOptions<SMTPConfigModel> smtpConfig) {
            _smtpConfig = smtpConfig.Value;
        }


        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Dear User, Confirm your email id.", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress,_smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach(var toemails in userEmailOptions.ToEmails)
            {
                mail.To.Add(toemails);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatepath, templateName));
            return body;
        }
    }
}
