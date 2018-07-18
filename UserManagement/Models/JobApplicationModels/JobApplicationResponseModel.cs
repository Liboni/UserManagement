
namespace UserManagement.Models.JobApplicationModels
{
    using UserManagement.Models.JobModels;
    using UserManagement.Models.OrganisationProfileModels;
    using UserManagement.Models.UserProfileModels;

    public class JobApplicationResponseModel
    {
        public int Id { get; set; }
        public JobResponseModel Job { get; set; }
        public UserProfileResponseModel Applicant { get; set; }
        public OrganisationProfileResponseModel Organisation { get; set; }
        public bool IsDeleted { get; set; }
    }
}
