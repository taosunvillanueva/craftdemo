namespace RegistrationAPI.BusinessLogic
{
    using RegistrationAPI.Models;
    using System;
    using System.Threading.Tasks;

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
    }
}