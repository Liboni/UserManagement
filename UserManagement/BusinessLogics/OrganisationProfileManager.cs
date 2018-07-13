
namespace UserManagement.BusinessLogics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using UserManagement.Data;
    using UserManagement.LocalObjects;
    using UserManagement.Models.OrganisationProfileModels;

    public class OrganisationProfileManager
    {
        private readonly ApplicationDbContext context;

        public OrganisationProfileManager(ApplicationDbContext context)
        {
            this.context = context;
        }

        public GenericActionResult<string> SaveOrganisationProfile(OrganisationProfileModel organisationProfileModel)
        {
            try
            {
                context.OrganisationProfiles.Add(new OrganisationProfile
                                                     {
                                                        CompanyAddress = organisationProfileModel.CompanyAddress,
                                                        DateCreated = DateTime.Now,
                                                        UserId = organisationProfileModel.UserId,
                                                        CompanyBusinessType = organisationProfileModel.CompanyBusinessType,
                                                        CompanyName = organisationProfileModel.CompanyName,
                                                        CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber,
                                                        CompanyRegistrationId = organisationProfileModel.CompanyRegistrationId,
                                                        CountryId = organisationProfileModel.CountryId,
                                                        DateOfCompanyRegistration = organisationProfileModel.DateOfCompanyRegistration
                                                     });
                context.SaveChanges();
                return new GenericActionResult<string>(true,"");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<string> UpdateOrganisationProfile(OrganisationProfileModel organisationProfileModel)
        {
            try
            {
                OrganisationProfile organisationProfile = context.OrganisationProfiles.Find(organisationProfileModel.Id);
                organisationProfile.CompanyAddress = organisationProfileModel.CompanyAddress;
                organisationProfile.CompanyBusinessType = organisationProfileModel.CompanyBusinessType;
                organisationProfile.CompanyName = organisationProfileModel.CompanyName;
                organisationProfile.CompanyPhoneNumber = organisationProfileModel.CompanyPhoneNumber;
                organisationProfile.CompanyRegistrationId = organisationProfileModel.CompanyRegistrationId;
                organisationProfile.CountryId = organisationProfileModel.CountryId;
                organisationProfile.DateOfCompanyRegistration = organisationProfileModel.DateOfCompanyRegistration;
                context.SaveChanges();
                return new GenericActionResult<string>(true, "");
            }
            catch (Exception exception)
            {
                return new GenericActionResult<string>(exception.Message);
            }
        }

        public GenericActionResult<OrganisationProfile> GetOrganisationProfileById(string userId)
        {
            try
            {
                return new GenericActionResult<OrganisationProfile>(true, "", context.OrganisationProfiles.FirstOrDefault(a=>a.UserId.Equals(userId)));
            }
            catch (Exception exception)
            {
                return new GenericActionResult<OrganisationProfile>(exception.Message);
            }
        }

        public GenericActionResult<List<OrganisationProfile>> GetOrganisationProfiles()
        {
            try
            {
                return new GenericActionResult<List<OrganisationProfile>>(true, "", context.OrganisationProfiles.ToList());
            }
            catch (Exception exception)
            {
                return new GenericActionResult<List<OrganisationProfile>>(exception.Message);
            }
        }
    }
}
