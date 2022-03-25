using BangOnERP.DataBase;
using BangOnERP.GlobalEncryptionDecryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoles.Business.Contexctual;

namespace UserRoles.Service
{
    public class UserTypeRepository : IUserType
    {
        private readonly BangOnERPContext db;
       
        public UserTypeRepository(BangOnERPContext _db)
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
                    UserLoginType delUser = db.UserLoginTypes.Find(Convert.ToInt32(idstr));
                    db.UserLoginTypes.Remove(delUser);
                    db.SaveChanges();
                    transaction.Commit();
                    var gh = (db.UserLoginTypes.ToList().Select(m => new
                    {
                        UserLoginType = m.UserLoginType1.ToString(),
                        UserLoginTypeId = EncryptionDecryption.Encryption(m.UserLoginTypeId.ToString()),
                        Active=m.Active,
                        CreatedBy = m.CreatedBy.ToString(),
                        CreatedDate = m.CreatedDate.ToString(),
                        UpdatedBy = m.UpdatedBy.ToString(),
                        UpdatedDate = m.UpdatedDate.ToString()
                        
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
                var gh = db.UserLoginTypes.Find(Convert.ToInt32(idstr));
               
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
                var gh = (db.UserLoginTypes.ToList().Select(m => new
                {
                    UserLoginType = m.UserLoginType1.ToString(),
                    UserLoginTypeId = EncryptionDecryption.Encryption(m.UserLoginTypeId.ToString()),
                    Active = m.Active,
                    CreatedBy = m.CreatedBy.ToString(),
                    CreatedDate = m.CreatedDate.ToString(),
                    UpdatedBy = m.UpdatedBy.ToString(),
                    UpdatedDate = m.UpdatedDate.ToString()

                }));
                return gh;
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }

        public string Inserted(UserLoginType obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    obj.CreatedDate = DateTime.Now;
                    db.UserLoginTypes.Add(obj);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.UserLoginTypeId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public string Updated(UserLoginTypeCustomer obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    UserLoginType getFrmId = db.UserLoginTypes.AsNoTrackingWithIdentityResolution().
                    Single(m => m.UserLoginTypeId ==Convert.ToInt32(obj.UserLoginTypeId));

                    getFrmId.UpdatedDate = DateTime.Now;
                    getFrmId.UserLoginType1 = obj.UserLoginType1;
                    getFrmId.Active = obj.Active;

                    db.UserLoginTypes.Update(getFrmId);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(getFrmId.UserLoginTypeId.ToString());
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
