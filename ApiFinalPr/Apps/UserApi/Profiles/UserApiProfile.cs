using ApiFinalPr.Apps.UserApi.DTOs.AccountDto;
using ApiFinalPr.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.UserApi.Profiles
{
    public class UserApiProfile:Profile
    {
        public UserApiProfile()
        {
            CreateMap<AppUser, AccountGetDto>();
        }
    }
}
