using AlpataTech.MeetingAppDemo.Entities.DTO.User;

namespace AlpataTech.MeetingAppDemo.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> GenerateJWT(UserAuthDto userAuthDto);
    }
}
