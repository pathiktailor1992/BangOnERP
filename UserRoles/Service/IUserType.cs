using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangOnERP.DataBase;
using UserRoles.Business.Contexctual;

namespace UserRoles.Service
{
    public interface IUserType
    {
        object GetAll();
        string Inserted(UserLoginType obj);
        object Edited(string id);
        string Updated(UserLoginTypeCustomer obj);
        object Deleted(string id);
    }
}
