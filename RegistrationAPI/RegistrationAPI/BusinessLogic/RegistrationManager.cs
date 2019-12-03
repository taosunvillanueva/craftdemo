namespace RegistrationAPI.BusinessLogic
{
    using Newtonsoft.Json.Linq;
    using RegistrationAPI.Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public sealed class RegistrationManager
    {
        /// <summary>
        /// By default, there is no offset
        /// </summary>
        private const int defaultPage = 0;

        /// <summary>
        /// By default, we get maximum 10 items
        /// </summary>
        private const int defaultPerPage = 10;

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
            var jProperty = new JProperty("result", registrasionsJson);
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

        public async Task<JObject> GetRegistrationSort(string sort, string pageValue, string perPageValue)
        {
            int page = defaultPage;
            if (!string.IsNullOrEmpty(pageValue) && !int.TryParse(pageValue, out page))
            {
                throw new ArgumentException("page is not a number. page is: " + page);
            }

            int perPage = defaultPerPage;
            if (!string.IsNullOrEmpty(perPageValue) && !int.TryParse(perPageValue, out perPage))
            {
                throw new ArgumentException("perPage is not a number. perPage is: " + perPage);
            }

            var sql = this.dbManager.BuildSQLSortQuery(sort, page * perPage, perPage);
            if (!string.IsNullOrEmpty(sql))
            {
                return await this.RunSql<Registration>(sql);
            }

            return null;
        }
    }
}