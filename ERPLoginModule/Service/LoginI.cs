using BangOnERP.DataBase;
using ERPLoginModule.Service.Contextual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPLoginModule.Service
{
   public interface LoginI
    {
        Task<List<LoginUser>> GetAll();
        Task<string> Inserted(LoginSuperLogin obj);
        Task<VwLoginLoginUser> Edited(string id);
        Task<string> Updated(LoginUser obj);
        Task<List<LoginUser>> Deleted(string id);
        string Login(string userName, string password);
    }
}
