namespace RegistrationAPI.Controllers
{
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
        public async Task<HttpResponseMessage> MakeRegistration([FromBody]UserRegistry userRegistry)
        {
            try
            {
                var result = await RegistrationManager.Instance.TryRegister(userRegistry);

                if (result)
                {
                    // Standard status code 201 for POST succesfully created new item
                    var message = Request.CreateResponse(HttpStatusCode.Created, userRegistry);

                    // Todo: the real URI of the newly created item
                    message.Headers.Location = new Uri(Request.RequestUri + userRegistry.Name);
                    return message;
                }

                // TODO: update this response status later
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }

}
