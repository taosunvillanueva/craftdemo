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

        public async Task<UserRegistry> TryRegister(UserRegistry userRegister)
        {
            try
            {
                userRegister.IsUpdated = false;
                var existingRegistry = await this.CheckIfUserRegistryExists(userRegister);
                var result = existingRegistry == null ?
                    await dbManager.AddUserRegistryAsync(userRegister) :
                    await dbManager.UpdateUserRegisterAsync(existingRegistry, userRegister);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private async Task<UserRegistry> CheckIfUserRegistryExists(UserRegistry userRegister)
        {
            try
            {
                var result = await this.dbManager.CheckItemExistsAsync(userRegister);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}