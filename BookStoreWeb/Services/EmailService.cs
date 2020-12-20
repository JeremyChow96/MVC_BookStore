using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using Microsoft.Extensions.Options;

namespace BookStoreWeb.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPConfigModel _smtpConfig;
        private const string templatePath = @"EmailTemplate/{0}.html";

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "This is a email test";  //邮箱主题
            // userEmailOptions.Body = GetEmailBody("TestEmail");
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders); //邮箱内容

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, Confirm your email id",
                userEmailOptions.PlaceHolders);
            // userEmailOptions.Body = GetEmailBody("TestEmail");
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForForgoPassword(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, reset your password",
                userEmailOptions.PlaceHolders);
            // userEmailOptions.Body = GetEmailBody("TestEmail");
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject =  userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From =  new MailAddress(_smtpConfig.SenderAddress,_smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHtml
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
             mail.To.Add(toEmail);   
            }
            
            NetworkCredential newNetworkCredential = new NetworkCredential(_smtpConfig.UserName,_smtpConfig.Password);
            
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials =  newNetworkCredential
            };

            mail.BodyEncoding = Encoding.Default;
            smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath,templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text)&&keyValuePairs!=null)
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
    }
}