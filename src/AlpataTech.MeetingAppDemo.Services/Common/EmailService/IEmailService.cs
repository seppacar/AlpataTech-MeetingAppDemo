using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;

namespace AlpataTech.MeetingAppDemo.Services.Common.EmailService
{
    public interface IEmailService
    {
        string ReadEmailTemplate(string templateName);
        Task SendEmailAsync(string to,  string subject, string body);
        Task SendWelcomeEmailAsync(string to, string userName);
        Task SendMeetingCreatedEmailAsync(string to, MeetingDto meetingDto);
        Task SendMeetingParticipationEmailAsync(string to, string subject, string body);
        Task SendMeetingNotificationAsync(string to, string subject, string body);
    }
}
