using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GMS.Backend.API.POC.GMS.API.DAL;

namespace GMS.Backend.API.POC.Controllers
{
    [Authorize]
    public class UserAuthenticationController : ApiController
    {      
        public IEnumerable<string> GetDetails()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public void GetAuthenticateUser(string UserName,String Password)
        {
            User userValidation = new User();
            //userValidation.ConfigureAuth();

        }

    }
}