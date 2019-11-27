using Newtonsoft.Json;
using System;

namespace RegistrationAPI.Models
{
    public class Registration
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OfficeLocation { get; set; }
        public string SecurityInterest { get; set; }
        public string ShirtSize { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}