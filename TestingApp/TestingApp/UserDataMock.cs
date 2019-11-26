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

        public static UserRegistry FetchSampleUserData()
        {
            return FetchData();
        }

        private static UserRegistry FetchData()
        {
            var name = "Tao444";
            var email = "tao.sun.toto@gmail.com";
            var officeLocation = "San Diego";
            var securityInterest = "Getting Ahead of Attackers";
            var shirtSize = "Men M";

            return new UserRegistry()
            {
                Name = name,
                Email = email,
                OfficeLocation = officeLocation,
                SecurityInterest = securityInterest,
                ShirtSize = shirtSize
            };
        }
    }
}
