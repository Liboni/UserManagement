
namespace UserManagement.BusinessLogics
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using UserManagement.Data;
    using UserManagement.Enums;
    using UserManagement.Models;
    using UserManagement.Models.BusinessTypeModels;
    using UserManagement.Models.JobApplicationModels;
    using UserManagement.Models.JobModels;
    using UserManagement.Models.OrganisationProfileModels;
    using UserManagement.Models.UserCreditModels;
    using UserManagement.Models.UserProfileModels;
    using UserManagement.Models.ValuesModels;

    public class ObjectConverterManager
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public ObjectConverterManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserProfileResponseModel> ToUserProfileResponseModel(UserProfile userProfile, string webRootPath)
        {
            ApplicationUser user = await userManager.FindByIdAsync(userProfile.UserId);
            return new UserProfileResponseModel{
                           Id = userProfile.Id,
                           ProfileImage = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(webRootPath, userProfile.ProfileImageName))),
                           Gender = new GenderModel { Id = Convert.ToByte(userProfile.Gender), Name = Enum.GetName(typeof(Gender), userProfile.Gender) },
                           DateOfBirth = userProfile.DateOfBirth,
                           FirstName = userProfile.FirstName,
                           CountryId = userProfile.CountryId,
                           LastName = userProfile.LastName,
                           Country = context.Countries.Find(userProfile.CountryId),
                           PhoneNumber = user.PhoneNumber,
                           UserId = user.Id,
                           UserName = user.UserName,
                           Email = user.Email,
                           DateCreated = user.DateCreated
            };
        }

        public async Task<OrganisationProfileResponseModel> ToOrganisationProfileModel(OrganisationProfile organisationProfile, string webRootPath)
        {
            ApplicationUser user = await userManager.FindByIdAsync(organisationProfile.UserId);
            return new OrganisationProfileResponseModel
            {
                           UserId = user.Id,
                           Id = organisationProfile.Id,
                           ProfileImage = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(webRootPath, organisationProfile.ProfileImageName))),
                           Email = user.Email,
                           UserName = user.UserName,
                           Phonenumber = user.PhoneNumber,
                           DateCreated = organisationProfile.DateCreated,
                           CompanyAddress = organisationProfile.CompanyAddress,
                           Country = context.Countries.Find(organisationProfile.CountryId),
                           CompanyPhoneNumber = organisationProfile.CompanyPhoneNumber,
                           CompanyName = organisationProfile.CompanyName,
                           BusinessType = context.BusinessTypes.Find(organisationProfile.BusinessTypeId),
                           DateOfCompanyRegistration = organisationProfile.DateOfCompanyRegistration,
                           CompanyRegistrationId = organisationProfile.CompanyRegistrationId
                       };
        }

        public JobResponseModel ToJobResponseModel(Job job, string webRootPath)
        {
            return new JobResponseModel{
                           ProfileImage = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(webRootPath, job.ProfileImageName))),
                           Organisation = new OrganisationProfileManager(context,userManager).GetOrganisationProfileById(job.UserId,webRootPath).Data,
                           Talent = context.Talents.Find(job.TalentId),
                           DueDate = job.DueDate,
                           Compensation = job.Compensation,
                           Description = job.Description,
                           Disabled = job.Disabled,
                           Name = job.Name,
                           Address = job.Address,
                           Id = job.Id,
                           Gender = new GenderModel { Id = Convert.ToByte(job.Gender), Name = Enum.GetName(typeof(Gender), job.Gender) }
                        };
        }

        public JobApplicationResponseModel ToJobApplicationResponseModel(JobApplication jobApplication, string webRootPath)
        {
            return new JobApplicationResponseModel
                       {
                           Applicant = new UserProfileManager(context,userManager).GetUserDetailsByUserId(jobApplication.UserId, webRootPath).Data,
                           Job = new JobManager(context,userManager).GetJob(webRootPath,jobApplication.JobId).Data,
                           Organisation = new OrganisationProfileManager(context,userManager).GetOrganisationProfileById(jobApplication.UserId,webRootPath).Data,
                           Id = jobApplication.Id,
                           IsDeleted = jobApplication.IsDeleted
                       };
        }

        public static BusinessType ToBusinessType(BusinessTypeModel businessTypeModel)
        {
            return new BusinessType
                       {
                           Id = businessTypeModel.Id,
                           Name = businessTypeModel.Name,
                           IsDeleted = false
                       };
        }
        public static OrganisationProfileModel ToOrganisationProfileModel(AddOrganisationProfileModel organisationProfileModel, string userId)
        {
            return new OrganisationProfileModel
            {
                          CompanyAddress = organisationProfileModel.CompanyAddress,
                          CompanyBusinessType = organisationProfileModel.CompanyBusinessType,
                          CompanyName = organisationProfileModel.CompanyName,
                          CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber,
                          CompanyRegistrationId = organisationProfileModel.CompanyRegistrationId,
                          CountryId = organisationProfileModel.CountryId,
                          DateOfCompanyRegistration = organisationProfileModel.DateOfCompanyRegistration,
                          ProfileImage = organisationProfileModel.ProfileImage,
                          UserId = userId
                       };
        }
        public static BusinessTypeModel ToBusinessTypeModel(BusinessType businessType)
        {
            return new BusinessTypeModel
            {
                           Id = businessType.Id,
                           Name = businessType.Name
                       };
        }

        public  UserCreditResponseModel ToUserCreditResponseModel(UserCredit userCredit)
        {
            return new UserCreditResponseModel
                       {
                           Talent = context.Talents.Find(userCredit.TalentId),
                           UserId = userCredit.UserId,
                           Production = userCredit.Production,
                           Id = userCredit.Id,
                           IsDeleted = userCredit.IsDeleted,
                           DateCreated = userCredit.DateCreated
                       };
        }

        public static UserProfileModel ToUserProfileModel(AddUserProfileModel userProfileModel, string userId)
        {
            return new UserProfileModel
                       {
                           CountryId = userProfileModel.CountryId,
                           DateOfBirth = userProfileModel.DateOfBirth,
                           FirstName = userProfileModel.FirstName,
                           Gender = userProfileModel.Gender,
                           LastName = userProfileModel.LastName,
                           ProfileImage = userProfileModel.ProfileImage,
                           UserId = userId
                       };
        }
    }
}
