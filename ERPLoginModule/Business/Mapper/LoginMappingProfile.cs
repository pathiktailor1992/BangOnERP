using AutoMapper;
using BangOnERP.DataBase;
using BangOnERP.GlobalEncryptionDecryption;
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
            
            CreateMap<VwLoginLoginUser, LoginUser>()
                             .ForMember(dest =>dest.LoginSuperLoginId, act => act.MapFrom(src =>EncryptionDecryption.Encryption(src.LoginSuperLoginId.ToString())))
                             .ForMember(dest => dest.LoginUsername, act => act.MapFrom(src => src.LoginUsername))
                             .ForMember(dest => dest.LoginActive, act => act.MapFrom(src => src.LoginActive))
                             .ForMember(dest => dest.LoginCreatedDate, act => act.MapFrom(src =>Convert.ToString(src.LoginCreatedDate)))
                             .ForMember(dest => dest.LoginCreatedBy, act => act.MapFrom(src => Convert.ToString(src.LoginCreatedBy.ToString())))
                             .ForMember(dest => dest.LoginUpdatedDate, act => act.MapFrom(src => Convert.ToString(src.LoginUpdatedDate.ToString())))
                             .ForMember(dest => dest.LoginUpdatedBy, act => act.MapFrom(src => Convert.ToString(src.LoginUpdatedBy.ToString())));

           
        }
    }
}
