using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;

namespace AlpataTech.MeetingAppDemo.Services.Common.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string smtpUsername;
        private readonly string smtpPassword;
        private readonly IConfiguration _configuration;
        
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Retrieve SMTP settings from configuration
            smtpServer = _configuration["EmailSettings:SmtpServer"];
            smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            smtpPassword = _configuration["EmailSettings:SmtpPassword"];
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                var message = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    To = { to },
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await client.SendMailAsync(message);
            }
        }

        public string ReadEmailTemplate(string templateName)
        {
            // Construct the path to the template based on the provided template name
            string templatePath = Path.Combine("Resources", "EmailTemplates", $"{templateName}.html");
            // Read the email template from a file
            return File.ReadAllText(templatePath);
        }

        public async Task SendWelcomeEmailAsync(string to, UserDto userDto)
        {
            string emailTemplate = ReadEmailTemplate("UserWelcome");
            string body = emailTemplate.Replace("{{FirstName}}", userDto.FirstName).Replace("{{LastName}}", userDto.LastName);

            await SendEmailAsync(to,"Welcome to [App Name Here]", body);
        }
        public Task SendMeetingCreatedEmailAsync(string to, MeetingDto meetingDto)
        {
            throw new NotImplementedException();
        }

        public Task SendMeetingParticipationEmailAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendMeetingNotificationAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
