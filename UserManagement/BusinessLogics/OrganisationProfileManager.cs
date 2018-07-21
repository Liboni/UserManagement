
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
    using UserManagement.Models.OrganisationProfileModels;

    public class OrganisationProfileManager
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public OrganisationProfileManager(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<GenericActionResult<OrganisationProfile>> SaveOrganisationProfile(AddOrganisationProfileModel organisationProfileModel, string webRootPath, string userId)
        {
            try
            {
                if (context.OrganisationProfiles.FirstOrDefault(a => a.UserId.Equals(userId)) != null)
                    return await UpdateOrganisationProfile(ObjectConverterManager.ToOrganisationProfileModel(organisationProfileModel,userId), webRootPath);
                var profile = new OrganisationProfile
                            {
                                CompanyAddress = organisationProfileModel.CompanyAddress,
                                DateCreated = DateTime.Now,
                                UserId = userId,
                                BusinessTypeId = organisationProfileModel.CompanyBusinessType,
                                CompanyName = organisationProfileModel.CompanyName,
                                CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber,
                                CompanyRegistrationId =organisationProfileModel.CompanyRegistrationId,
                                CountryId = organisationProfileModel.CountryId,
                                DateOfCompanyRegistration =organisationProfileModel.DateOfCompanyRegistration,
                                ProfileImageName =await UploadFile.SaveFileInWebRoot(organisationProfileModel.ProfileImage,webRootPath)
                            };
                context.OrganisationProfiles.Add(profile);
                context.SaveChanges();
                return new GenericActionResult<OrganisationProfile>(true, "Organisation profile saved successfully.", profile);
            }
            catch (Exception)
            {
                return new GenericActionResult<OrganisationProfile>("Failed to save organisation profile, please try again or contact the administrator.");
            }
        }

        public async Task<GenericActionResult<OrganisationProfile>> UpdateOrganisationProfile(OrganisationProfileModel organisationProfileModel, string webRootPath)
        {
            try
            {
                OrganisationProfile organisationProfile = context.OrganisationProfiles.Find(organisationProfileModel.Id);
                organisationProfile.CompanyAddress = organisationProfileModel.CompanyAddress;
                organisationProfile.BusinessTypeId = organisationProfileModel.CompanyBusinessType;
                organisationProfile.CompanyName = organisationProfileModel.CompanyName;
                organisationProfile.CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber;
                organisationProfile.CompanyRegistrationId = organisationProfileModel.CompanyRegistrationId;
                organisationProfile.CountryId = organisationProfileModel.CountryId;
                organisationProfile.DateOfCompanyRegistration = organisationProfileModel.DateOfCompanyRegistration;
                organisationProfile.ProfileImageName =await UploadFile.SaveFileInWebRoot(organisationProfileModel.ProfileImage, webRootPath);
                context.SaveChanges();
                return new GenericActionResult<OrganisationProfile>(true, "Organisation profile updated successfully.", organisationProfile);
            }
            catch (Exception)
            {
                return new GenericActionResult<OrganisationProfile>("Failed to update organisation profile, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<OrganisationProfileResponseModel> GetOrganisationProfileById(string userId, string webRootPath)
        {
            try
            {
                return new GenericActionResult<OrganisationProfileResponseModel>(true, "", context.OrganisationProfiles.Where(a=>a.UserId.Equals(userId) && !a.IsDeleted).Select(organisation=> new ObjectConverterManager(context,userManager).ToOrganisationProfileModel(organisation,webRootPath).Result).FirstOrDefault());
            }
            catch (Exception)
            {
                return new GenericActionResult<OrganisationProfileResponseModel>("Failed to get organisation profile, please try again or contact the administrator.");
            }
        }

        public GenericActionResult<OrganisationProfile> DeleteOrganisationProfile(int id)
        {
            try
            {
                var organisationProfile = context.OrganisationProfiles.Find(id);
                organisationProfile.IsDeleted = true;
                context.SaveChanges();
                return new GenericActionResult<OrganisationProfile>(true, "Organisation profile deleted successfully.", organisationProfile);
            }
            catch (Exception)
            {
                return new GenericActionResult<OrganisationProfile>("Failed to delete organisation profile, please try again or contact the administrator.");
            }
        }


        public GenericActionResult<List<OrganisationProfileResponseModel>> GetOrganisationProfiles(string webRootPath, int skip, int take)
        {
            try
            {
                List<OrganisationProfile> profiles = context.OrganisationProfiles.Where(a=>!a.IsDeleted).Skip(skip).Take(take).ToList();
                return new GenericActionResult<List<OrganisationProfileResponseModel>>(true, "", profiles.Select(organisationProfile => new ObjectConverterManager(context, userManager).ToOrganisationProfileModel(organisationProfile, webRootPath).Result).ToList());
            }
            catch (Exception)
            {
                return new GenericActionResult<List<OrganisationProfileResponseModel>>("Failed to get organisation profile, please try again or contact the administrator.");
            }
        }
    }
}
