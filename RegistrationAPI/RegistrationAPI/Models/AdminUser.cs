namespace RegistrationAPI.Models
{
    using Newtonsoft.Json;
    using System;

    public class AdminUser : AdminUserSimple
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public new Byte[] Password { get; set; }

        public void CreateId()
        {
            this.Id = this.Username + ".1";
        }

        public string GetUserId()
        {
            return this.Username + ".1";
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}