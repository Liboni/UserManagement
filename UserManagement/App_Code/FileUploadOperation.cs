
namespace UserManagement
{
    using System.Collections.Generic;
    using System.Linq;

    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    using UserManagement.Controllers;

    public class FileUploadOperation : IOperationFilter
    {
        private readonly IEnumerable<string> actionsWithUpload = new[]
                                                                      {
                                                                          "Api"+NamingHelpers.GetOperationId<OrganisationProfileController>(nameof(OrganisationProfileController.Post)),
                                                                          "Api"+NamingHelpers.GetOperationId<JobController>(nameof(JobController.Post)),
                                                                          "Api"+NamingHelpers.GetOperationId<UserProfileController>(nameof(UserProfileController.Post))
                                                                      };
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (actionsWithUpload.Contains(operation.OperationId))
            {
                operation.Parameters.Remove(new NonBodyParameter{Name = "ProfileImage" });
                operation.Parameters.Add(new NonBodyParameter
                                             {
                                                 Name = "ProfileImage",
                                                 In = "formData",
                                                 Description = "Upload Image",
                                                 Required = false,
                                                 Type = "file"
                                             });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
