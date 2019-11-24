namespace RegistrationAPI.BusinessLogic
{
    using Microsoft.Azure.Cosmos.Table;
    using RegistrationAPI.Models;
    using System;
    using System.Threading.Tasks;

    public sealed class RegistrationManager
    {
        private static readonly Lazy<RegistrationManager> lazy = new Lazy<RegistrationManager>(() => new RegistrationManager());

        public static RegistrationManager Instance { get { return lazy.Value; } }

        private RegistrationManager()
        {
        }

        public async Task<bool> TryRegister(UserRegistry userRegister)
        {
            var table = CloudStorage.CreateTableAsync("userregistry1");

            // Todo: check database has the user already

            var registry = await InsertRegistryAsync(table, userRegister);

            // Todo: return the correct value
            return true;
        }

        public static async Task<UserRegistry> InsertRegistryAsync(CloudTable table, UserRegistry userRegistry)
        {
            if (userRegistry == null)
            {
                throw new ArgumentNullException("userRegistry");
            }
            try
            {
                // Create the Insert table operation
                var insertOperation = TableOperation.Insert(userRegistry);

                // Execute the operation.
                var result = await table.ExecuteAsync(insertOperation);
                var insertedRegistry = result.Result as UserRegistry;

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure Cosmos DB
                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Insert Operation: " + result.RequestCharge);
                }

                return insertedRegistry;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
    }
}