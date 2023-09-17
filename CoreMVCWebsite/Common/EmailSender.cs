using Microsoft.AspNetCore.Identity.UI.Services;

namespace CoreMVCWebsite.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // add login to send email.
            return Task.CompletedTask;
        }
    }
}
