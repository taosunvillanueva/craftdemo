namespace TestingApp
{
    using Newtonsoft.Json;
    using RegistrationAPI.Models;

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
            var name = "Tao444";
            var email = "tao.sun.toto@gmail.com";
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

        public static AdminUserTemp CreateAdminUser()
        {
            var name = "Tao";
            var password = "TaoAdmin";

            var admin = new AdminUserTemp()
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
