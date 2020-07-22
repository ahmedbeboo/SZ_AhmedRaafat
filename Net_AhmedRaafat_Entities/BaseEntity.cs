using Newtonsoft.Json;
using System;

namespace Net_AhmedRaafat_Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            //CreatedDate = DateTime.Now;

        }
    }
}
