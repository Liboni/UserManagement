
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Linq;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.JobModels;

    public class JobManager
    {
        private readonly ApplicationDbContext context;

        public JobManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericActionResult<string> SaveJob(JobModel jobModel)
        {
            try
            {
                context.Jobs.Add(new Job
                {
                    Compensation = jobModel.Compensation,
                    DateCreated = DateTime.Now,
                    Location = jobModel.Location,
                    Description = jobModel.Description,
                    TalentId = jobModel.TalentId,
                    UserId = jobModel.UserId,
                    Name = jobModel.Name,
                    Disabled = jobModel.Disabled,
                    DueDate = jobModel.DueDate,
                    Gender = jobModel.Gender
                });
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Job saved successfully.");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to save job, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<string> UpdateJob(JobModel jobModel)
        {
            try
            {
                Job job = context.Jobs.Find(jobModel.Id);
                job.Compensation = jobModel.Compensation;
                job.Location = jobModel.Location;
                job.Description = jobModel.Description;
                job.TalentId = jobModel.TalentId;
                job.UserId = jobModel.UserId;
                job.Name = jobModel.Name;
                job.Disabled = jobModel.Disabled;
                job.DueDate = jobModel.DueDate;
                job.Gender = jobModel.Gender;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "Job updated successfully.");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to update job, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<string> DeleteJob(int jobId)
        {
            try
            {
                context.JobApplications.Remove(context.JobApplications.Find(jobId));
                context.SaveChanges();
                return new GenericActionResult<string>(true,"Job deleted successfully");
            }
            catch (Exception)
            {
                return new GenericActionResult<string>("Failed to delete job, please try again or contact the administrator.");
            }
        }
    }
}
