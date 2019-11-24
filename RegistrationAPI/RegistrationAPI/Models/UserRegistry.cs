using Microsoft.Azure.Cosmos.Table;

namespace RegistrationAPI.Models
{
    public class UserRegistry : TableEntity
    {
        public UserRegistry(string name, string email, string location, string interest, string size)
        {
            PartitionKey = location;
            RowKey = name;

            this.Name = name;
            this.Email = email;
            this.OfficeLocation = location;
            this.SecurityInterest = interest;
            this.ShirtSize = size;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string OfficeLocation { get; set; }
        public string SecurityInterest { get; set; }
        public string ShirtSize { get; set; }
    }
}