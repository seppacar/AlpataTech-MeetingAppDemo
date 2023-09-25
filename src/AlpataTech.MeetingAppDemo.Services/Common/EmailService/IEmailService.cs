using AlpataTech.MeetingAppDemo.Entities.DTO.Meeting;
using AlpataTech.MeetingAppDemo.Entities.DTO.User;

namespace AlpataTech.MeetingAppDemo.Services.Common.EmailService
{
    public interface IEmailService
    {
        string ReadEmailTemplate(string templateName);
        Task SendEmailAsync(string to,  string subject, string body);
        Task SendWelcomeEmailAsync(string to, UserDto userDto);
        Task SendMeetingCreatedEmailAsync(string to, MeetingDto meetingDto);
        Task SendMeetingParticipationEmailAsync(string to, string subject, string body);
        Task SendMeetingNotificationAsync(string to, string subject, string body);
    }
}
