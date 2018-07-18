
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.JobApplicationModels;

    public class JobApplicationManager
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public JobApplicationManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public GenericActionResult<string> SaveJobApplication(JobApplicationModel jobApplicationModel)
        {
            try
            {
                context.JobApplications.Add(new JobApplication{
                            UserId = jobApplicationModel.UserId,
                            DateCreated = DateTime.Now,
                            IsDeleted = false,
                            JobId = jobApplicationModel.JobId
                        });
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Job application saved successfully.");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to save job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<string> UpdateJobApplication(JobApplicationModel jobApplicationModel)
        {
            try
            {
                var jobApplication = context.JobApplications.Find(jobApplicationModel.Id);
                jobApplication.IsDeleted = jobApplicationModel.IsDeleted;
                jobApplication.JobId = jobApplicationModel.JobId;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Job application updated successfully.");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to updated job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<string> DeleteJobApplication(int id)
        {
            try
            {
                var jobApplication = context.JobApplications.Find(id);
                jobApplication.IsDeleted = true;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Job application delete successfully.");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to delete job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<List<JobApplicationResponseModel>> GetAllJobApplications(string webRootPath, int from, int count)
        {
            try
            {
                var applications = context.JobApplications.Where(a => !a.IsDeleted).Skip(from).Take(count).ToList();
                return new GenericActionResult<List<JobApplicationResponseModel>>(true,"", applications.Select(a=>new ObjectConverter(context, userManager).ToJobApplicationResponseModel(a,webRootPath)).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<JobApplicationResponseModel>>("Failed to get job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<List<JobApplication>> GetJobApplicationByUserId(string userId)
        {
            try
            {
                return new GenericActionResult<List<JobApplication>>(true, "", context.JobApplications.Where(a => !a.IsDeleted && a.UserId.Equals(userId)).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<JobApplication>>("Failed to get job application, please try again or contact the administrator.");
            }
        }
    }
}
