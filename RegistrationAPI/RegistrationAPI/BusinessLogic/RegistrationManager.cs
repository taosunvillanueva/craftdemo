namespace RegistrationAPI.BusinessLogic
{
    using Newtonsoft.Json.Linq;
    using RegistrationAPI.Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public sealed class RegistrationManager
    {
        private static readonly Lazy<RegistrationManager> lazy = new Lazy<RegistrationManager>(() => new RegistrationManager());

        private DatabaseManager dbManager;

        public static RegistrationManager Instance { get { return lazy.Value; } }

        private RegistrationManager()
        {
            this.dbManager = new DatabaseManager();
            this.dbManager.SetupDbClient();
        }

        public async Task<Registration> RegisterUser(Registration userRegister)
        {
            var registeredUser = await dbManager.AddUserRegistryAsync(userRegister);
            return registeredUser;
        }

        public async Task<AdminUser> AddAdminUser(AdminUser adminUser)
        {
            var createdAdminUser = await dbManager.AddAdminUserAsync(adminUser);
            return createdAdminUser;
        }

        public async Task<bool> VerifyAdmin(AdminUser adminUser)
        {
            var adminPasswordMatch = await dbManager.VerifyAdminUserAsync(adminUser);
            return adminPasswordMatch;
        }

        public async Task<JObject> GetRegistrationByCity(string city)
        {
            var registrations = await dbManager.GetRegistrationByCityAsync(city);

            var jObject = new JObject();
            var registrasionsJson = new JavaScriptSerializer().Serialize(registrations);
            var jProperty = new JProperty("registrations", registrasionsJson);
            jObject.Add(jProperty);

            return jObject;
        }

        public async Task<JObject> RunSql<T>(string sql)
        {
            var sqlResult = await dbManager.RunSQLAsync<T>(sql);

            var jObject = new JObject();
            var resultJson = new JavaScriptSerializer().Serialize(sqlResult);
            var jProperty = new JProperty("result", resultJson);
            jObject.Add(jProperty);

            return jObject;
        }
    }
}