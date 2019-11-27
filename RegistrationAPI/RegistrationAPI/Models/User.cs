namespace RegistrationAPI.Models
{
    using Newtonsoft.Json;
    using System;

    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Email { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}