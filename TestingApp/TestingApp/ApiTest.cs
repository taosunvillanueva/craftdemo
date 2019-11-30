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
            //var userData = UserDataMock.FetchSampleUserData();
            //var testResult = RegisterAsync(userData).Result;

            //var admin = UserDataMock.CreateAdminUser();
            //var adminResult = AddAdminAsync(admin).Result;
            //var adminResult = VerifyAdminAsync(admin).Result;

            //var registrationsResult = GetRegistrationsByCityAsync("San Diego").Result;

            var sqlResult = RunSqlAsync().Result;
        }

        private async Task<bool> RegisterAsync(RegistrationAPI.Models.Registration userRegistry)
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;

            var json = JsonConvert.SerializeObject(userRegistry);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync($"Api/RegisterUser", stringContent);

            if (response.IsSuccessStatusCode)
                result = response.StatusCode;

            return result == HttpStatusCode.Created;
        }

        private async Task<bool> AddAdminAsync(AdminUserSimple adminUser)
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;

            var json = JsonConvert.SerializeObject(adminUser);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync($"Api/AddAdmin", stringContent);

            if (response.IsSuccessStatusCode)
                result = response.StatusCode;

            return result == HttpStatusCode.Created;
        }

        private async Task<bool> VerifyAdminAsync(AdminUserSimple adminUser)
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;

            var json = JsonConvert.SerializeObject(adminUser);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync($"Api/AdminLogin", stringContent);

            if (response.IsSuccessStatusCode)
                result = response.StatusCode;

            return result == HttpStatusCode.Created;
        }

        private async Task<bool> GetRegistrationsByCityAsync(string city)
        {
            HttpStatusCode result = HttpStatusCode.BadRequest;

            var response = await client.GetAsync($"Api/RegistrationsByCity/" + city);

            if (response.IsSuccessStatusCode)
                result = response.StatusCode;

            return result == HttpStatusCode.Created;
        }

        private async Task<string> RunSqlAsync()
        {
            var sql = "SELECT * FROM c Order By c.OfficeLocation OFFSET 2 LIMIT 2";

            var json = JsonConvert.SerializeObject(sql);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await client.PostAsync($"Api/TestRegistrationSQL", stringContent);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return content;

            return null;
        }
    }
}
