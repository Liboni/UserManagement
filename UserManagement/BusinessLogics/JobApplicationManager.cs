
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
        public GenericActionResult<JobApplication> SaveJobApplication(JobApplicationModel jobApplicationModel)
        {
            try
            {
                var application = new JobApplication
                                      {
                                          UserId = jobApplicationModel.UserId,
                                          DateCreated = DateTime.Now,
                                          IsDeleted = false,
                                          JobId = jobApplicationModel.JobId,
                                          IsViewed = false
                                      };
                context.JobApplications.Add(application);
                context.SaveChanges();
                return new GenericActionResult<JobApplication>(true, "Job application saved successfully.", application);
            }
            catch (Exception)
            {
                return new GenericActionResult<JobApplication>("Failed to save job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<JobApplication> UpdateJobApplication(JobApplicationModel jobApplicationModel)
        {
            try
            {
                var jobApplication = context.JobApplications.Find(jobApplicationModel.Id);
                jobApplication.IsDeleted = jobApplicationModel.IsDeleted;
                jobApplication.JobId = jobApplicationModel.JobId;
                context.SaveChanges();
                return new GenericActionResult<JobApplication>(true, "Job application updated successfully.", jobApplication);
            }
            catch (Exception)
            {
                return new GenericActionResult<JobApplication>("Failed to updated job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<JobApplication> DeleteJobApplication(int id)
        {
            try
            {
                var jobApplication = context.JobApplications.Find(id);
                jobApplication.IsDeleted = true;
                context.SaveChanges();
                return new GenericActionResult<JobApplication>(true, "Job application delete successfully.", jobApplication);
            }
            catch (Exception)
            {
                return new GenericActionResult<JobApplication>("Failed to delete job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<JobApplication> ViewJobApplication(int id)
        {
            try
            {
                var jobApplication = context.JobApplications.Find(id);
                jobApplication.IsViewed = true;
                context.SaveChanges();
                return new GenericActionResult<JobApplication>(true, "Job application viewed successfully.", jobApplication);
            }
            catch (Exception)
            {
                return new GenericActionResult<JobApplication>("Failed to viewed job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<JobApplicationResponseModel> GetJobApplications(string webRootPath, int id)
        {
            try
            {
                return new GenericActionResult<JobApplicationResponseModel>(true, "", context.JobApplications.Where(a => !a.IsDeleted && a.Id == id).Select(a => new ObjectConverterManager(context, userManager).ToJobApplicationResponseModel(a, webRootPath)).FirstOrDefault());
            }
            catch (Exception)
            {
                return new GenericActionResult<JobApplicationResponseModel>("Failed to get job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<List<JobApplicationResponseModel>> GetAllJobApplications(string webRootPath, int from, int count)
        {
            try
            {
                var applications = context.JobApplications.Where(a => !a.IsDeleted).Skip(from).Take(count).ToList();
                return new GenericActionResult<List<JobApplicationResponseModel>>(true,"", applications.Select(a=>new ObjectConverterManager(context, userManager).ToJobApplicationResponseModel(a,webRootPath)).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<JobApplicationResponseModel>>("Failed to get job application, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<List<JobApplicationResponseModel>> GetJobApplicationByUserId(string userId, string webRootPath, int from, int count)
        {
            try
            {
                var applications = context.JobApplications.Where(a => !a.IsDeleted && a.UserId.Equals(userId)).Skip(from).Take(count).ToList();
                return new GenericActionResult<List<JobApplicationResponseModel>>(true, "", applications.Select(a => new ObjectConverterManager(context, userManager).ToJobApplicationResponseModel(a, webRootPath)).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<JobApplicationResponseModel>>("Failed to get job application, please try again or contact the administrator.");
            }
        }
    }
}
