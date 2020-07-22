using Net_AhmedRaafat_Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net_AhmedRaafat.BL
{
    public class ApplicationUserDtocs:BaseEntity
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("emailConfirmed")]
        public bool EmailConfirmed { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string PasswordHash { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("isDeleted")]
        public bool isDeleted { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
