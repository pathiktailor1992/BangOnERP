using BangOnERP.DataBase;
using MasterLocation.Business.Contexctual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterLocation.Service
{
   public interface IState
    {
        object GetAll();
        string Inserted(MasterStateCustomer obj);
        object Edited(string id);
        string Updated(MasterStateCustomer obj);
        object Deleted(string id);
    }
}
