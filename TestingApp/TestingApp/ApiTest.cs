namespace TestingApp
{
    using Newtonsoft.Json;
    using RegistrationAPI.Models;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiTest
    {
        private static HttpClient client;
        private const string url = "https://registrationapi20191122063201.azurewebsites.net/";

        public ApiTest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void PerformTest()
        {
            var userData = UserDataMock.FetchSampleUserData();
            var testResult = RegisterAsync(userData).Result;
        }

        private async Task<bool> RegisterAsync(UserRegistry userRegistry)
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;

            var json = JsonConvert.SerializeObject(userRegistry);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync($"Api/RegisterUser", stringContent);

            if (response.IsSuccessStatusCode)
                result = response.StatusCode;

            return result == HttpStatusCode.Created;
        }
    }
}
