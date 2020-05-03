using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Net_AhmedRaafat_Entities;

namespace Net_AhmedRaafat.BL
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, ApplicationUserDtocs>();                // means you want to map from User to UserDTO
            CreateMap<ApplicationUserDtocs, ApplicationUser>();                // means you want to map from UserDTO to User
            CreateMap<List<ApplicationUserDtocs>, List<ApplicationUser>>();    // means you want to map from List Of UserDTO to List Of User
        }
    }

}
