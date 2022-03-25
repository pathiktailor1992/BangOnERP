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
    public class CountryRepository : ICountry
    {
        private readonly BangOnERPContext db;

        public CountryRepository(BangOnERPContext _db)
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
                    BangOnERP.DataBase.MasterCountry delUser = db.MasterCountries.Find(Convert.ToInt32(idstr));
                    db.MasterCountries.Remove(delUser);
                    db.SaveChanges();
                    transaction.Commit();
                    var gh = (db.MasterCountries.ToList().Select(m => new {
                        CountryId = EncryptionDecryption.Encryption(m.CountryId.ToString()),
                        CountryName = m.CountryName,
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
                var gh = db.MasterCountries.Find(Convert.ToInt32(idstr));

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
                var gh = (db.MasterCountries.ToList().Select(m=>new {
                    CountryId=  EncryptionDecryption.Encryption(m.CountryId.ToString()),
                    CountryName= m.CountryName,
                    CreatedDate=m.CreatedDate.ToString()
                }));
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public string Inserted(BangOnERP.DataBase.MasterCountry obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    obj.CreatedDate = DateTime.Now;
                    db.MasterCountries.Add(obj);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.CountryId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public string Updated(MasterCountryCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                   BangOnERP.DataBase.MasterCountry getFrmId = db.MasterCountries.AsNoTrackingWithIdentityResolution().
                    Single(m => m.CountryId ==Convert.ToInt32(obj.CountryId));

                    getFrmId.UpdatedDate = DateTime.Now;
                    getFrmId.CountryName = obj.CountryName;
                  
                    db.MasterCountries.Update(getFrmId);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.CountryId.ToString());
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
