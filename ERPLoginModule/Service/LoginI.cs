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
        object GetAll();
        string Inserted(LoginSuperLogin obj);
        object Edited(string id);
        string Updated(LoginUser obj);
        object Deleted(string id);
        string Login(string userName, string password);
    }
}
