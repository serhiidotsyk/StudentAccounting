using BLL.Models.StudentProfile;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMailService
    {
        public Task SendConfirmationLinkAsync(string email, string body);
        public Task<string> GenerateConfirmationLinkAsync(UserModel userModel);
        public Task<string> GenerateEmailConfirmationTokenAsync(UserModel userModel);
        public void SendScheduledEmail(string email, string message);
    }
}
