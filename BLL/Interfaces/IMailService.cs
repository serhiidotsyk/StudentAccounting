using BLL.Models.StudentProfile;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMailService
    {
        /// <summary>
        /// Sends email with confirmational link
        /// </summary>
        /// <param name="email"></param>
        /// <param name="body"></param>
        public Task SendConfirmationLinkAsync(string email, string body);

        /// <summary>
        /// Generates confirmational link
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// Confirmational link
        /// </returns>
        public Task<string> GenerateConfirmationLinkAsync(UserModel userModel);

        /// <summary>
        /// Generates token for confirmational link
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>
        /// Confirmation token
        /// </returns>
        public Task<string> GenerateEmailConfirmationTokenAsync(UserModel userModel);

        /// <summary>
        /// Sends email for scheduling jobs
        /// </summary>
        /// <param name="email"></param>
        /// <param name="message"></param>
        public void SendScheduledEmail(string email, string message);
    }
}
