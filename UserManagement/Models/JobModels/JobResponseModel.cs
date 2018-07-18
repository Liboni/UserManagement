
namespace UserManagement.Models.JobModels
{
    using System;

    using UserManagement.Data;
    using UserManagement.Models.OrganisationProfileModels;
    using UserManagement.Models.ValuesModels;

    public class JobResponseModel
    {
        public int Id { get; set; }
        public OrganisationProfileResponseModel Organisation { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Talent Talent { get; set; }
        public string Compensation { get; set; }
        public GenderModel Gender { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public DateTime DueDate { get; set; }
        public bool Disabled { get; set; }
    }
}
