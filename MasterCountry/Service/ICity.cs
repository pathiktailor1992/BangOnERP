using BangOnERP.DataBase;
using MasterLocation.Business.Contexctual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterLocation.Service
{
    public  interface ICity
    {
        object GetAll();
        string Inserted(MasterCityCustomer obj);
        object Edited(string id);
        string Updated(MasterCityCustomer obj);
        object Deleted(string id);
        object GetStatesByCountry(string id);
        object GetAllCountry();
    }
}
