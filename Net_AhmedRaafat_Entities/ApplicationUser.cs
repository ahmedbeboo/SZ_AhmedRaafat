using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Net_AhmedRaafat_Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //public bool confirmed { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        public bool isDeleted { get; set; }
    }
}
