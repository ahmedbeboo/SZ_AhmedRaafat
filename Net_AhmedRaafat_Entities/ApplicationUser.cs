using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Net_AhmedRaafat_Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public bool confirmed { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public bool isDeleted { get; set; }
    }
}
