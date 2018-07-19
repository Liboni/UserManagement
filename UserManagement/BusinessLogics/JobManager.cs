
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models;
    using UserManagement.Models.JobModels;

    public class JobManager
    {
        private readonly ApplicationDbContext context;

        private readonly UserManager<ApplicationUser> userManager;

        public JobManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<GenericActionResult<Job>> SaveJob(JobModel jobModel, string webRootPath)
        {
            try
            {
                var job = new Job
                              {
                                  Compensation = jobModel.Compensation,
                                  DateCreated = DateTime.Now,
                                  Address = jobModel.Address,
                                  Description = jobModel.Description,
                                  TalentId = jobModel.TalentId,
                                  UserId = jobModel.UserId,
                                  Name = jobModel.Name,
                                  Disabled = jobModel.Disabled,
                                  DueDate = jobModel.DueDate,
                                  Gender = jobModel.Gender,
                                  CountryId = jobModel.CountryId,
                                  ProfileImageName =
                                      await UploadFile.SaveFileInWebRoot(jobModel.ProfileImage, webRootPath)
                              };
                context.Jobs.Add(job);
                context.SaveChanges();
                return new GenericActionResult<Job>(true, "Job saved successfully.", job);
            }
            catch (Exception)
            {
                return new GenericActionResult<Job>("Failed to save job, please try again or contact the administrator.");
            }
        }

        public async Task<GenericActionResult<Job>> UpdateJob(JobModel jobModel, string webRootPath)
        {
            try
            {
                Job job = context.Jobs.Find(jobModel.Id);
                job.Compensation = jobModel.Compensation;
                job.Address = jobModel.Address;
                job.Description = jobModel.Description;
                job.TalentId = jobModel.TalentId;
                job.UserId = jobModel.UserId;
                job.Name = jobModel.Name;
                job.Disabled = jobModel.Disabled;
                job.DueDate = jobModel.DueDate;
                job.Gender = jobModel.Gender;
                job.CountryId = jobModel.CountryId;
                job.ProfileImageName = await UploadFile.SaveFileInWebRoot(jobModel.ProfileImage, webRootPath);
                context.SaveChanges();
                return new GenericActionResult<Job>(true, "Job updated successfully.", job);
            }
            catch (Exception)
            {
                return new GenericActionResult<Job>("Failed to update job, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<Job> DeleteJob(int jobId)
        {
            try
            {
                var job = context.Jobs.Find(jobId);
                context.Jobs.Remove(job);
                context.SaveChanges();
                return new GenericActionResult<Job>(true,"Job deleted successfully", job);
            }
            catch (Exception)
            {
                return new GenericActionResult<Job>("Failed to delete job, please try again or contact the administrator.");
            }
        }
        public GenericActionResult<JobResponseModel> GetJob(string webRootPath,int id)
        {
            try
            {
                return new GenericActionResult<JobResponseModel>(true, "", context.Jobs.Where(a=>a.Id==id).Select(jobModel => new ObjectConverterManager(context, userManager).ToJobResponseModel(jobModel, webRootPath)).FirstOrDefault());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<JobResponseModel>(exception.Message);
            }
        }

        public GenericActionResult<List<JobResponseModel>> GetJobs(string webRootPath,int from, int count)
        {
            try
            {
                List<Job> jobs = context.Jobs.Skip(from).Take(count).ToList();
                return new GenericActionResult<List<JobResponseModel>>(true,"",jobs.Select(jobModel=> new ObjectConverterManager(context, userManager).ToJobResponseModel(jobModel, webRootPath)).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<JobResponseModel>>(exception.Message);
            }
        }

        public GenericActionResult<List<JobResponseModel>> GetJobs(string webRootPath,string userId, int from, int count)
        {
            try
            {
                List<Job> jobs = context.Jobs.Where(a=>a.UserId.Equals(userId)).Skip(from).Take(count).ToList();
                return new GenericActionResult<List<JobResponseModel>>(true, "", jobs.Select(jobModel => new ObjectConverterManager(context, userManager).ToJobResponseModel(jobModel, webRootPath)).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<JobResponseModel>>(exception.Message);
            }
        }

        public GenericActionResult<List<JobResponseModel>> GetJobs(string webRootPath,int countryId,int genderId, int talentId, int from, int count)
        {
            try
            {
                List<Job> jobs = context.Jobs.Where(a => a.Gender == (genderId==0?a.Gender:genderId)
                                                    && a.TalentId == (talentId==0?a.TalentId:talentId) 
                                                    && a.CountryId == (countryId==0?a.CountryId:countryId))
                                                    .Skip(from).Take(count).ToList();
            return new GenericActionResult<List<JobResponseModel>>(true, "", jobs.Select(jobModel => new ObjectConverterManager(context, userManager).ToJobResponseModel(jobModel, webRootPath)).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<JobResponseModel>>(exception.Message);
            }
        }
    }
}
