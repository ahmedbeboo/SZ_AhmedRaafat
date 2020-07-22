using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Net_AhmedRaafat_Entities
{
    public abstract class Item:BaseEntity
    {
        
        public string text { get; set; }

        public string picturesUrl { get; set; }


        [ForeignKey("User")]
        public Guid userId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
