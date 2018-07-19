
namespace UserManagement
{
    using System;

    using Microsoft.AspNet.Identity;
    using Microsoft.Extensions.Configuration;

    using UserManagement.BusinessLogics;
    using UserManagement.Data;
    using UserManagement.Models;
    using UserManagement.Services;

    public class EmailNotification
    {
        private readonly ApplicationDbContext context;
        private IConfiguration Configuration { get; }

        public EmailNotification(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            Configuration = configuration;
        }

        public void SendRegistrationEmail(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            string tokenId = new TokenManager(context).SaveToken(
                new Token
                    {
                        User = user,
                        UserToken = userManager.GenerateEmailConfirmationTokenAsync(user)
                            .Result,
                        ExpiryDate = DateTime.Now.AddDays(1)
                    });
            string requestUrl = Configuration["FrontEndUrl:BaseUrl"] + Configuration["FrontEndUrl:RegistrationUrlPreffix"] + tokenId;
            IdentityMessage message = new IdentityMessage { Body = requestUrl, Destination = user.Email, Subject = "Registration Verification" };
            new EmailService().SendEmailAsync(message);
        }

        public void SendForgotPasswordEmail(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            string tokenId = new TokenManager(context).SaveToken(new Token
                    {
                        User = user,
                        UserToken = userManager.GeneratePasswordResetTokenAsync(user).Result,
                        ExpiryDate = DateTime.Now.AddDays(1)
                    });
            string requestUrl = Configuration["FrontEndUrl:BaseUrl"] + Configuration["FrontEndUrl:ForgotPasswordUrlPreffix"] + tokenId;
            IdentityMessage message = new IdentityMessage { Body = requestUrl, Destination = user.Email, Subject = "Forgot Password" };
            new EmailService().SendEmailAsync(message);
        }

        public void SendJobApplicationEmail(JobApplication jobApplication, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,string webRootPath)
        {
            var getJob = new JobManager(context, userManager).GetJob(webRootPath, jobApplication.JobId);
            var user = userManager.FindByIdAsync(getJob.Data.Organisation.UserId);
            var userDetails = new UserProfileManager(context, userManager).GetUserDetailsByUserId(jobApplication.UserId,webRootPath);
            string body = userDetails.Data.FirstName+" " +userDetails.Data.LastName+" has shown interest in the job for " + getJob.Data.Name+ " you advertised on JobSearch. to view more details on the application click this link."+ Configuration["FrontEndUrl:BaseUrl"] + Configuration["FrontEndUrl:ForgotPasswordUrlPreffix"] + jobApplication.Id;
            IdentityMessage message = new IdentityMessage { Body = body, Destination = user.Result.Email, Subject = "Job Application" };
            new EmailService().SendEmailAsync(message);
        }
    }
}
