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
    public class StateRepository : IState
    {
        private readonly BangOnERPContext db;

        public StateRepository(BangOnERPContext _db)
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
                    MasterState delUser = db.MasterStates.Find(Convert.ToInt32(idstr));
                    db.MasterStates.Remove(delUser);
                    db.SaveChanges();
                    transaction.Commit();
                    var gh = (db.MasterStates.ToList().Select(m => new {
                        StateId = EncryptionDecryption.Encryption(m.StateId.ToString()),
                        StateName = m.StateName,
                        CountryId= EncryptionDecryption.Encryption(m.CountryId.ToString()),
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
                var gh = db.MasterStates.Find(Convert.ToInt32(idstr));
                MasterStateCustomer starcust = new MasterStateCustomer();
                starcust.CountryId = EncryptionDecryption.Encryption(gh.CountryId.ToString());
                starcust.StateId = EncryptionDecryption.Encryption(gh.StateId.ToString());
                starcust.StateName = gh.StateName;

                return starcust;
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
                var gh = (db.VwStateLists.ToList().Select(m => new {
                    StateId = EncryptionDecryption.Encryption(m.StateId.ToString()),
                    StateName = m.StateName,
                    CountryId = EncryptionDecryption.Encryption(m.CountryId.ToString()),
                    CountryName=m.CountryName,
                    CreatedDate = m.CreatedDate.ToString()
                }));
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public string Inserted(MasterStateCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    MasterState masinser = new MasterState();
                    masinser.CreatedDate = DateTime.Now;
                    masinser.CountryId =Convert.ToInt32(EncryptionDecryption.Decryption(obj.CountryId));
                    masinser.StateName = obj.StateName;
                    db.MasterStates.Add(masinser);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(masinser.StateId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public string Updated(MasterStateCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    MasterState getFrmId = db.MasterStates.AsNoTrackingWithIdentityResolution().
                     Single(m => m.StateId.ToString() == obj.StateId);

                    getFrmId.UpdatedDate = DateTime.Now;
                    getFrmId.StateName = obj.StateName;
                    getFrmId.CountryId =Convert.ToInt32(EncryptionDecryption.Decryption(obj.CountryId));
                    db.MasterStates.Update(getFrmId);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.StateId.ToString());
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
