namespace TestingApp
{
    using Newtonsoft.Json;
    using RegistrationAPI.Models;
    using System.Collections.Generic;

    public static class UserDataMock
    {
        public static string FetchSampleUserDataAsJson()
        {
            return JsonConvert.SerializeObject(FetchData());
        }

        public static RegistrationAPI.Models.Registration FetchSampleUserData()
        {
            return FetchData();
        }

        private static RegistrationAPI.Models.Registration FetchData()
        {
            var name = "Tao1";
            var email = "tao11@gmail.com";
            var officeLocation = "San Diego";
            var securityInterest = "Getting Ahead of Attackers";
            var shirtSize = "Men M";

            return new RegistrationAPI.Models.Registration()
            {
                Name = name,
                Email = email,
                OfficeLocation = officeLocation,
                SecurityInterest = securityInterest,
                ShirtSize = shirtSize
            };
        }

        public static IList<RegistrationAPI.Models.Registration> FetchRandomUserData()
        {
            var result = new List<RegistrationAPI.Models.Registration>();
            for (var i = 0; i < 15; i++)
            {
                var registration = new RegistrationAPI.Models.Registration()
                {
                    Name = "someperson" + i,
                    Email = "someperson" + i + "@someperson" + i + ".com",
                    OfficeLocation = "Boston",
                    SecurityInterest = "Application Security",
                    ShirtSize = "Mens L"
                };

                result.Add(registration);
            }

            return result;
        }

        public static AdminUserSimple CreateAdminUser()
        {
            var name = "Tao";
            var password = "TaoAdmin";

            var admin = new AdminUserSimple()
            {
                Username = name,
                Password = password
            };

            return admin;
        }

        private static byte[] HashPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            var hashAlgorithm = new System.Security.Cryptography.SHA256Managed();
            var hash = hashAlgorithm.ComputeHash(data);

            return hash;
        }
    }
}
