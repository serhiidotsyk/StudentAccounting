using BLL.Models.StudentProfile;

namespace BLL.Interfaces
{
    public interface IMailService
    {
        public void SendConfirmationLink(string email, string body);
        public string GenerateConfirmationLink(UserModel userModel);
        public string GenerateEmailConfirmationToken(UserModel userModel);
        public void SendScheduledEmail(string email, string message);
    }
}
