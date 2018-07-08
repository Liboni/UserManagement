
namespace UserManagement.Controllers
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.AspNetCore.Mvc;

    using UserManagement.Models;

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserProfileController : Controller
    {
        [HttpPost]
        [Route("addUserDetails")]
        public IActionResult AddUserDetails([FromBody]UserDetailsModel userDetailsModel)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getUserDetails")]
        public UserDetailsModel GetUserDetails()
        {
            return new UserDetailsModel();
        }

        [HttpPost]
        [Route("getAllUserDetails")]
        public List<UserDetailsModel> GetAllUserDetails()
        {
            return new List<UserDetailsModel>();
        }

        [HttpPost]
        [Route("getUserDetails/{userId}")]
        public UserDetailsModel GetUserDetails(string userId)
        {
            return new UserDetailsModel();
        }

        [HttpPost]
        [Route("uploadProfile")]
        public IActionResult UploadProfile()
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getProfile/{userId}")]
        public IActionResult UploadProfile(string userId)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getProfile")]
        public Stream GetProfile()
        {
            return Stream.Synchronized(Stream.Null);
        }

        [HttpPost]
        [Route("getAllProfile")]
        public Stream GetAllProfile()
        {
            return Stream.Synchronized(Stream.Null);
        }

        [HttpPost]
        [Route("addCredits")]
        public IActionResult AddCredits([FromBody]CreditsModel creditsModel)
        {
            return Accepted();
        }

        [HttpPost]
        [Route("getCredits")]
        public CreditsModel GetCredits()
        {
            return new CreditsModel();
        }

        [HttpPost]
        [Route("getCredits/{userId}")]
        public CreditsModel GetCredits(string userId)
        {
            return new CreditsModel();
        }

        [HttpPost]
        [Route("getallCredits")]
        public List<CreditsModel> GetAllCredits()
        {
            return new List<CreditsModel>();
        }
    }
}