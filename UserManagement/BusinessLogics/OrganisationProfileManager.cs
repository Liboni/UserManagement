
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

        public async Task<GenericActionResult<string>> SaveOrganisationProfile(OrganisationProfileModel organisationProfileModel, string webRootPath)
        {
            try
            {
                context.OrganisationProfiles.Add(new OrganisationProfile
                                                     {
                                                        CompanyAddress = organisationProfileModel.CompanyAddress,
                                                        DateCreated = DateTime.Now,
                                                        UserId = organisationProfileModel.UserId,
                                                        BusinessTypeId = organisationProfileModel.CompanyBusinessType,
                                                        CompanyName = organisationProfileModel.CompanyName,
                                                        CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber,
                                                        CompanyRegistrationId = organisationProfileModel.CompanyRegistrationId,
                                                        CountryId = organisationProfileModel.CountryId,
                                                        DateOfCompanyRegistration = organisationProfileModel.DateOfCompanyRegistration,
                                                        ProfileImageName= await UploadFile.SaveFileInWebRoot(organisationProfileModel.ProfileImage, webRootPath)
                });
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public async Task<GenericActionResult<string>> UpdateOrganisationProfile(OrganisationProfileModel organisationProfileModel, string webRootPath)
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
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<OrganisationProfileResponseModel> GetOrganisationProfileById(string userId, string webRootPath)
        {
            try
            {
                return new GenericActionResult<OrganisationProfileResponseModel>(true, "", context.OrganisationProfiles.Where(a=>a.UserId.Equals(userId)).Select(organisation=> new ObjectConverter(context,userManager).ToOrganisationProfileModel(organisation,webRootPath).Result).FirstOrDefault());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<OrganisationProfileResponseModel>(exception.Message);
            }
        }

        public GenericActionResult<List<OrganisationProfileResponseModel>> GetOrganisationProfiles(string webRootPath, int from, int count)
        {
            try
            {
                List<OrganisationProfile> profiles = context.OrganisationProfiles.Skip(from).Take(count).ToList();
                return new GenericActionResult<List<OrganisationProfileResponseModel>>(true, "", profiles.Select(organisationProfile => new ObjectConverter(context, userManager).ToOrganisationProfileModel(organisationProfile, webRootPath).Result).ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<OrganisationProfileResponseModel>>(exception.Message);
            }
        }
    }
}
