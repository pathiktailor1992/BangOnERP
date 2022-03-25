using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangOnERP.DataBase;
using MasterLocation.Business.Contexctual;

namespace MasterLocation.Service
{
  public  interface ICountry
    {
        object GetAll();
        string Inserted(BangOnERP.DataBase.MasterCountry obj);
        object Edited(string id);
        string Updated(MasterCountryCustomer obj);
        object Deleted(string id);
    }
}
