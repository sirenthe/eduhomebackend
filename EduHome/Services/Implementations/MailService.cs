//using System.Net.Mail;
//using EduHome.Services.Interfaces;
//using EduHome.ViewModels;
//using MailKit.Net.Smtp;
//using MailKit.Security;
//using MimeKit;
//using MailKit.Security;
//using MimeKit;

//namespace EduHome.Services.Implementations
//{
//    public class MailService : IMailService
//    {
//        private readonly IConfiguration _configuration;
//        public MailService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }
//        public async Task SendEmailAsync(MailRequest mailRequest)
//        {
//            var email = new MimeMessage();
//            email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
//            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
//            email.Subject = mailRequest.Subject;
//            var builder = new BodyBuilder();
//            if (mailRequest.Attachments != null)
//            {
//                byte[] fileBytes;
//                foreach (var file in mailRequest.Attachments)
//                {
//                    if (file.Length > 0)
//                    {
//                        using (var ms = new MemoryStream())
//                        {
//                            file.CopyTo(ms);
//                            fileBytes = ms.ToArray();
//                        }
//                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
//                    }
//                }
//            }
//            builder.HtmlBody = mailRequest.Body;
//            email.Body = builder.ToMessageBody();
//            using var smtp = new SmtpClient();
//            smtp.Connect(_configuration["MailSettings:Host"], int.Parse((_configuration["MailSettings:Port"]), (System.Globalization.NumberStyles)SecureSocketOptions.StartTls));
//            smtp.Authenticate(_configuration["MailSettings:Mail"], (_configuration["MailSettings:Password"]));
//            await smtp.SendAsync(email);
//            smtp.Disconnect(true);
//        }
//    }
//}
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Security;
using EduHome.ViewModels;
using EduHome.Services.Interfaces;

namespace EduHome.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
     
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();


            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

          
            smtp.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
             
                return true; 
            };

            smtp.Connect(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
