using BLL.Models.Email;
using Microsoft.Extensions.Options;
using System;
using BLL.Models.StudentProfile;
using Microsoft.AspNetCore.Http;
using BLL.Interfaces;
using Microsoft.AspNetCore.Routing;
using System.Net.Mail;
using System.Net;
using DAL;
using System.Linq;

namespace BLL.Services
{
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IHttpContextAccessor _httpContext;
        private readonly LinkGenerator _linkGenerator;
        private readonly ApplicationDbContext _context;
        public MailService(IOptions<EmailConfiguration> emailConfig, IHttpContextAccessor httpContext, LinkGenerator linkGenerator, ApplicationDbContext context)
        {
            _emailConfig = emailConfig.Value;
            _httpContext = httpContext;
            _linkGenerator = linkGenerator;
            _context = context;
        }

        public MailService()
        {
        }

        public void SendConfirmationLink(string email, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_emailConfig.SenderAdress);
            message.To.Add(new MailAddress(email));
            message.Subject = _emailConfig.Subject;
            message.IsBodyHtml = true;
            message.Body = body;
            

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailConfig.SenderAdress, "1234pass");
                client.Send(message);
            }

        }

        public string GenerateConfirmationLink(UserModel userModel)
        {
            var token = GenerateEmailConfirmationToken(userModel);
            var callbackUrl = "https://" + _httpContext.HttpContext.Request.Host + _linkGenerator.GetPathByAction("ConfirmEmail", "auth", new { userId = userModel.Id, token = token });

            return callbackUrl;
        }


        public string GenerateEmailConfirmationToken(UserModel userModel)
        {
            var user = _context.Users.Where(u => u.Id == userModel.Id).SingleOrDefault();
            if (user != null)
            {
                string emailToken = Guid.NewGuid().ToString();
                user.EmailConfirmationToken = emailToken;
                
                _context.Users.Update(user);
                _context.SaveChanges();

                return emailToken;
            }
            return null;
        }

        public void SendScheduledEmail(string email, string sendMessage)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_emailConfig.SenderAdress);
            message.To.Add(new MailAddress(email));
            message.Subject = _emailConfig.Subject;
            message.Body = sendMessage;


            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_emailConfig.SenderAdress, "1234pass");
                client.Send(message);
            }
        }
    }
}
