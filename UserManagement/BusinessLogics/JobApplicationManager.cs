
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.JobApplicationModels;

    public class JobApplicationManager
    {
        private readonly ApplicationDbContext context;

        public JobApplicationManager(ApplicationDbContext context)
        {
            this.context = context;
        }
        public GenericActionResult<string> SaveJobApplication(JobApplicationModel jobApplicationModel)
        {
            try
            {
                context.JobApplications.Add(new JobApplication
                        {
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

        public GenericActionResult<List<JobApplication>> GetAllJobApplications()
        {
            try
            {
                return new GenericActionResult<List<JobApplication>>(true,"",context.JobApplications.Where(a=>!a.IsDeleted).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<JobApplication>>("Failed to get job application, please try again or contact the administrator.");
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
