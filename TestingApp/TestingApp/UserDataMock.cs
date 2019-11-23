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
            return new UserRegistry()
            {
                Name = "Tao",
                Email = "tao.sun.toto@me.com",
                OfficeLocation = "San Diego",
                SecurityInterest = "Getting Ahead of Attackers",
                ShirtSize = "Men M"
            };
        }
    }
}
