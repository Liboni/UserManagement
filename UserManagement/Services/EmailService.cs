
namespace UserManagement.Services
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    public class EmailService
    {
        public Task SendEmailAsync(IdentityMessage message)
        {
            try
            {
                MailMessage emailMessage = new MailMessage
                                               {
                                                   From = new MailAddress("libonisithole@gmail.com", "User management"),
                                                   IsBodyHtml = false,
                                                   Subject = message.Subject,
                                                   Body = message.Body,
                                                   Priority = MailPriority.Normal
                                               };
                emailMessage.To.Add(message.Destination);
                using (SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    mailClient.EnableSsl = true;
                    mailClient.Credentials = new System.Net.NetworkCredential("libonisithole@gmail.com", "%$Password123");
                    mailClient.Send(emailMessage);
                }
                  
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }
        }
    }
}
