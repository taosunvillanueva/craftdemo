namespace RegistrationAPI.Controllers
{
    using Microsoft.Azure.Cosmos;
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
                var userRegistry = userRegistryJson.ToObject<Registration>();

                var registeredUser = await RegistrationManager.Instance.RegisterUser(userRegistry);

                if (registeredUser != null)
                {
                    // Standard status code 201 for POST succesfully created new item
                    var message = Request.CreateResponse(HttpStatusCode.Created, registeredUser);
                    message.Headers.Location = new Uri(Request.RequestUri + userRegistry.Id);
                    return message;
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "User registration failed");
            }
            catch (CosmosException cosmosEx)
            {
                return Request.CreateErrorResponse(cosmosEx.StatusCode, cosmosEx);
            }
            catch (Exception otherEx)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, otherEx);
            }
        }

        [HttpPost]
        [Route("Api/AddAdmin")]
        public async Task<HttpResponseMessage> AddAdminUser([FromBody]JObject adminUserJson)
        {
            try
            {
                var adminUser =  AdminUserHelper.ConertToAdminUserDatabaseEntry(adminUserJson);

                var addedAdmin = await RegistrationManager.Instance.AddAdminUser(adminUser);

                if (addedAdmin != null)
                {
                    // Standard status code 201 for POST succesfully created new item
                    var message = Request.CreateResponse(HttpStatusCode.Created, addedAdmin);
                    message.Headers.Location = new Uri(Request.RequestUri + addedAdmin.Id);
                    return message;
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to add the admin username: " + adminUser.Username);
            }
            catch (CosmosException cosmosEx)
            {
                return Request.CreateErrorResponse(cosmosEx.StatusCode, cosmosEx);
            }
            catch (Exception otherEx)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, otherEx);
            }
        }

        [HttpPost]
        [Route("Api/AdminLogin")]
        public async Task<IHttpActionResult> AdminLogin([FromBody]JObject adminCredentialJson)
        {
            try
            {
                var adminCredential = AdminUserHelper.ConertToAdminUserDatabaseEntry(adminCredentialJson);

                var adminPasswordMatch = await RegistrationManager.Instance.VerifyAdmin(adminCredential);

                if (adminPasswordMatch)
                {
                    var token = AdminUserHelper.CreateToken(adminCredential.Username);

                    return Ok<JObject>(token);
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }

}
