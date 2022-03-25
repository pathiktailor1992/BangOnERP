using AutoMapper;
using BangOnERP.DataBase;
using ERPLoginModule.Service.Contextual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPLoginModule.Business.Mapper
{
    public class LoginMappingProfile:Profile
    {
        public LoginMappingProfile()
        {
            CreateMap<VwLoginLoginUser, LoginUser>(); 
        }
    }
}
