using BangOnERP.DataBase;
using BangOnERP.GlobalEncryptionDecryption;
using MasterLocation.Business.Contexctual;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterLocation.Service
{
    public class CityRepository : ICity
    {
        private readonly BangOnERPContext db;

        public CityRepository(BangOnERPContext _db)
        {
            db = _db;
        }
        public object Deleted(string id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string idstr = EncryptionDecryption.Decryption(id);
                    MasterCity delUser = db.MasterCities.Find(Convert.ToInt32(idstr));
                    db.MasterCities.Remove(delUser);
                    db.SaveChanges();
                    transaction.Commit();
                    var gh = (db.VwCityLists.ToList().Select(m => new {
                        StateName = m.StateName,
                        CountryName = m.CountryName,
                        CityId = EncryptionDecryption.Encryption(m.CityId.ToString()),
                        CityName = m.CityName,
                        CreatedDate = m.CreatedDate.ToString()
                    }));
                    return gh;

                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public object Edited(string id)
        {
            try
            {
                string idstr = EncryptionDecryption.Decryption(id);
                var gh = db.MasterCities.ToList().Where(m=>m.CityId==Convert.ToInt32(idstr)).Select(m=>new { 
                    CityId= EncryptionDecryption.Encryption(m.CityId.ToString()),
                    CityName= m.CityName,
                    StateId= EncryptionDecryption.Encryption(m.StateId.ToString()),
                    CountryId= EncryptionDecryption.Encryption(db.MasterStates.Find(m.StateId).CountryId.ToString())
                });
                
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public object GetAll()
        {
            try
            {
                var kh = from CityList in db.MasterCities join
                              StateList in db.MasterStates on CityList.StateId equals StateList.StateId into CityState from
                              StateList in CityState.DefaultIfEmpty()
                              select new { StateList.StateId};

                var gh = (db.VwCityLists.ToList().Select(m => new {
                  
                    StateName = m.StateName,
                    CountryName = m.CountryName,
                    CityId=EncryptionDecryption.Encryption(m.CityId.ToString()),
                    CityName=m.CityName,
                    CreatedDate = m.CreatedDate.ToString()
                }));
                return kh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public object GetStatesByCountry(string id)
        {
            try
            {
                var gh = (db.MasterStates.ToList().Where(m=>m.CountryId==Convert.ToInt32(EncryptionDecryption.Decryption(id))).Select(m => new {

                    StateName = m.StateName,
                    StateId = EncryptionDecryption.Encryption(m.StateId.ToString()),
                   
                }));
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public object GetAllCountry()
        {
            try
            {
                var gh = (db.MasterCountries.ToList()).Select(m => new {

                    CountryName = m.CountryName,
                    CountryId = EncryptionDecryption.Encryption(m.CountryId.ToString()),

                });
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public string Inserted(MasterCityCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    MasterCity masinser = new MasterCity();
                    masinser.CreatedDate = DateTime.Now;
                    masinser.StateId = Convert.ToInt32(EncryptionDecryption.Decryption(obj.StateId));
                    masinser.CityName = obj.CityName;
                    db.MasterCities.Add(masinser);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(masinser.CityId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public string Updated(MasterCityCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    MasterCity getFrmId = db.MasterCities.AsNoTrackingWithIdentityResolution().
                     Single(m => m.CityId.ToString() == obj.CityId);

                    getFrmId.UpdatedDate = DateTime.Now;
                    getFrmId.CityName = obj.CityName;
                    getFrmId.StateId = Convert.ToInt32(EncryptionDecryption.Decryption(obj.StateId));
                    db.MasterCities.Update(getFrmId);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.CityId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }
    }
}
