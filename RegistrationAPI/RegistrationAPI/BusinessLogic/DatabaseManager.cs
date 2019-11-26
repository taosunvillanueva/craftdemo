using Microsoft.Azure.Cosmos;
using RegistrationAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RegistrationAPI.BusinessLogic
{
    public class DatabaseManager
    {
        // TODO: put the secrets into KeyVault
        public static readonly string EndpointUri = "https://taocraftdemo.documents.azure.com:443/";
        public static readonly string PrimaryKey = "oLOQcEzzvyAGS5kIX7y7wdcK4cSrmhmUMDs78dgOwrIBf35adQfR83vRKtCNmxbuoGCeknFDXCy5EiRSmkSTyw==";
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        private string databaseId = "demodb";
        private string containerId = "userregistry1";

        public void SetupDbClient()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            this.database = this.cosmosClient.GetDatabase(this.databaseId);
            this.container = this.database.GetContainer(this.containerId);
        }

        public async Task<UserRegistry> CheckItemExistsAsync(UserRegistry userRegistry)
        {
            var sql = $"SELECT Top 1 * FROM c WHERE c.Email = '{userRegistry.Email.ToLower()}'";

            var queryDefinition = new QueryDefinition(sql);
            var queryResultSetIterator =  this.container.GetItemQueryIterator<UserRegistry>(queryDefinition);

            if (queryResultSetIterator.HasMoreResults)
            {
                var currentResult = await queryResultSetIterator.ReadNextAsync();
                return currentResult.FirstOrDefault();
            }

            return null;
        }

        public async Task<UserRegistry> AddUserRegistryAsync(UserRegistry userRegistry)
        {
            try
            {
                userRegistry.CreateId();
                var response = await this.container.CreateItemAsync<UserRegistry>(userRegistry, new PartitionKey(userRegistry.OfficeLocation));
                return response.Resource;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Adding new user registry failed with exception: " + ex.Message);
                return null;
            }
        }

        public async Task<UserRegistry> UpdateUserRegisterAsync(UserRegistry existingRegistry, UserRegistry newUserRegistry)
        {
            try
            {
                newUserRegistry.CreateId();
                newUserRegistry.IsUpdated = true;
                var response = await this.container.ReplaceItemAsync<UserRegistry>(newUserRegistry, existingRegistry.Id);
                var updatedUserRegistry = response.Resource;

                return updatedUserRegistry;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Updating user registry failed with exception: " + ex.Message);
                return null;
            }
        }
    }
}