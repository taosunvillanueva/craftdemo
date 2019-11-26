namespace RegistrationAPI.Controllers
{
    using Newtonsoft.Json.Linq;
    using RegistrationAPI.BusinessLogic;
    using RegistrationAPI.Models;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class RegistrationController : ApiController
    {
        [HttpPost]
        [Route("Api/RegisterUser")]
        public async Task<HttpResponseMessage> MakeRegistration([FromBody]JObject userRegistryJson)
        {
            try
            {
                var userRegistry = userRegistryJson.ToObject<UserRegistry>();

                var result = await RegistrationManager.Instance.TryRegister(userRegistry);

                if (result != null)
                {
                    // Standard status code 201 for POST succesfully created new item
                    var message = Request.CreateResponse(HttpStatusCode.Created, result);

                    // Todo: the real URI of the newly created item
                    message.Headers.Location = new Uri(Request.RequestUri + userRegistry.Name);
                    return message;
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "User registration failed");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }

}
