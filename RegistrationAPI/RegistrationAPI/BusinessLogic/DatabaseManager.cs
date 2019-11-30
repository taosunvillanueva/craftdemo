namespace RegistrationAPI.BusinessLogic
{
    using Microsoft.Azure.Cosmos;
    using RegistrationAPI.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DatabaseManager
    {
        // TODO: put the secrets into KeyVault
        public static readonly string EndpointUri = "https://taocraftdemo.documents.azure.com:443/";
        public static readonly string PrimaryKey = "oLOQcEzzvyAGS5kIX7y7wdcK4cSrmhmUMDs78dgOwrIBf35adQfR83vRKtCNmxbuoGCeknFDXCy5EiRSmkSTyw==";
        private CosmosClient cosmosClient;
        private Database database;
        private string databaseId = "demodb";
        private Container userContainer;
        private Container registrationContainer;
        private Container adminContainer;
        private string userContainerId = "userinfo";
        private string registrationContainerId = "registration";
        private string adminContainerId = "admin";

        public void SetupDbClient()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            this.database = this.cosmosClient.GetDatabase(this.databaseId);
            this.userContainer = this.database.GetContainer(this.userContainerId);
            this.registrationContainer = this.database.GetContainer(this.registrationContainerId);
            this.adminContainer = this.database.GetContainer(this.adminContainerId);
        }

        public async Task<Registration> AddUserRegistryAsync(Registration registration)
        {
            // Create user. An new id will be generated and assigned to the new user
            var user = new Models.User()
            {
                Email = registration.Email
            };

            registration.Id = user.Id;

            await this.userContainer.CreateItemAsync<Models.User>(user, new PartitionKey(user.Email));
            var registrationResponse = await this.registrationContainer.CreateItemAsync<Registration>(registration, new PartitionKey(registration.OfficeLocation));

            return registrationResponse.Resource;
        }

        public async Task<AdminUser> AddAdminUserAsync(AdminUser adminUser)
        {
            var adminUserResponse = await this.adminContainer.CreateItemAsync<AdminUser>(adminUser, new PartitionKey(adminUser.Username));
            return adminUserResponse.Resource;
        }

        public async Task<bool> VerifyAdminUserAsync(AdminUser adminUser)
        {
            var adminOnDbResponse = await this.adminContainer.ReadItemAsync<AdminUser>(adminUser.GetUserId(), new PartitionKey(adminUser.Username));
            return adminOnDbResponse.Resource.Password.SequenceEqual(adminUser.Password);
        }

        public async Task<IList<Registration>> GetRegistrationByCityAsync(string city)
        {
            var sql = "SELECT * FROM c WHERE c.OfficeLocation = '" + city + "'";
            var registrations = await this.RunSQLAsync<Registration>(sql);

            return registrations;
        }

        public async Task<IList<T>> RunSQLAsync<T>(string sql)
        {
            var option = new QueryRequestOptions();
            QueryDefinition queryDefinition = new QueryDefinition(sql);
            FeedIterator<T> queryResultSetIterator = this.registrationContainer.GetItemQueryIterator<T>(queryDefinition);

            IList<T> items = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T registration in currentResultSet)
                {
                    items.Add(registration);
                }
            }

            return items;
        }
    }
}