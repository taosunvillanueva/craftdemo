namespace RegistrationAPI.BusinessLogic
{
    using Microsoft.Azure.Cosmos.Table;
    using RegistrationAPI.App_Start;
    using System;

    public class CloudStorage
    {
        public static CloudTable CreateTableAsync(string tableName)
        {
            string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }

        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;

            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}