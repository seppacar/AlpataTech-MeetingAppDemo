using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;
using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities;

namespace AlpataTech.MeetingAppDemo.Services.Common.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string appName = "AlpataTech Demo APP"; // TODO: initalize it using appsettings.json
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

            await SendEmailAsync(to, $"Welcome to {appName}", body);
        }
        public async Task SendMeetingCreatedEmailAsync(string to, MeetingDto meetingDto)
        {
            string emailTemplate = ReadEmailTemplate("MeetingCreated");
            string body = emailTemplate
                .Replace("{{Title}}", meetingDto.Title)
                .Replace("{{OrganizerName}}", meetingDto.Organizer.FirstName + " " + meetingDto.Organizer.LastName)
                .Replace("{{Description}}", meetingDto.Description)
                .Replace("{{StartTime}}", meetingDto.StartTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{EndTime}}", meetingDto.EndTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{AppName}}", appName);

            await SendEmailAsync(to, $"Your meeting titled {meetingDto.Title} has been successfully created - {appName}", body);
        }

        public async Task SendMeetingParticipationEmailAsync(string to, MeetingDto meetingDto, MeetingParticipant meetingParticipant)
        {
            string emailTemplate = ReadEmailTemplate("MeetingParticipation");
            string body = emailTemplate
                .Replace("{{Title}}", meetingDto.Title)
                .Replace("{{ParticipantName}}", meetingParticipant.User.FirstName + " " + meetingParticipant.User.LastName)
                .Replace("{{Description}}", meetingDto.Description)
                .Replace("{{StartTime}}", meetingDto.StartTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{EndTime}}", meetingDto.EndTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{AppName}}", appName);

            await SendEmailAsync(to, $"You've been added as a participant in a meeting - {appName}", body);
        }

        public async Task SendMeetingNotificationAsync(string to, MeetingDto meetingDto, MeetingParticipant meetingParticipant)
        {
            string emailTemplate = ReadEmailTemplate("MeetingNotification");

            string body = emailTemplate
                .Replace("{{Title}}", meetingDto.Title)
                .Replace("{{ParticipantName}}", meetingDto.Organizer.FirstName + " " + meetingDto.Organizer.LastName)
                .Replace("{{StartTime}}", meetingDto.StartTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{EndTime}}", meetingDto.EndTime.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{AppName}}", appName);

            await SendEmailAsync(to, "Welcome to [App Name Here]", body);
        }
    }
}
