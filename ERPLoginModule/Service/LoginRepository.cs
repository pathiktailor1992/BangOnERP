using AutoMapper;
using BangOnERP.DataBase;
using ERPLoginModule.Service.Contextual;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETCore.Encrypt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BangOnERP.GlobalEncryptionDecryption;
using System.Text;

namespace ERPLoginModule.Service
{
    public class LoginRepository : LoginI
    {
        private readonly BangOnERPContext db;
        private readonly IMapper mapper;


        public LoginRepository(BangOnERPContext _db,IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public  object Deleted(string id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string idstr = EncryptionDecryption.Decryption(id);
                    LoginSuperLogin delUser = db.LoginSuperLogins.Find(Convert.ToInt32(idstr));
                    db.LoginSuperLogins.Remove(delUser);
                    db.SaveChanges();
                    transaction.Commit();

                    var attachments = mapper.Map<List<VwLoginLoginUser>, List<LoginUser>>(db.VwLoginLoginUsers.ToList());
                    return attachments;

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
                string idstr= EncryptionDecryption.Decryption(id);
                var gh = db.LoginSuperLogins.FromSqlRaw("GetSuperLoginByID" + " " + idstr).ToList().FirstOrDefault();
                gh.LoginSuperLoginId = 0;
                gh.LoginPassword = EncryptionDecryption.Decryption(gh.LoginPassword);
                return mapper.Map<LoginSuperLogin>(gh);
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
                var attachments = mapper.Map<List<VwLoginLoginUser>,List<LoginUser>>(db.VwLoginLoginUsers.ToList());
                return attachments;
            }
            catch(Exception ee)
            {
                return ee.ToString();
            }
        }

        public string Inserted(LoginSuperLogin obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    obj.LoginPassword = EncryptionDecryption.Encryption(obj.LoginPassword);
                    obj.LoginCreatedDate = DateTime.Now;
                    db.LoginSuperLogins.Add(obj);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.LoginSuperLoginId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }

        public string Updated(LoginUser obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    LoginSuperLogin getFrmId = db.LoginSuperLogins.AsNoTrackingWithIdentityResolution().
                    Single(m => m.LoginSuperLoginId ==Convert.ToInt32(obj.LoginSuperLoginId));

                    getFrmId.LoginUpdatedDate = DateTime.Now;
                    getFrmId.LoginUsername = obj.LoginUsername;
                    getFrmId.LoginActive = obj.LoginActive;
                    db.LoginSuperLogins.Update(getFrmId);
                    db.SaveChanges();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.LoginSuperLoginId.ToString());
                }

                catch (Exception ee)
                {
                    transaction.Rollback();
                    return ee.ToString();
                }
            }
        }


        public string Login(string userName, string password)
        {
            try
            {
                var user = db.LoginSuperLogins.Where(m => m.LoginUsername == userName).FirstOrDefault();

                var userRole = db.UserLoginTypes.Find(user.UserLoginTypeId);
                // return null if user not found
                if (user == null || password != EncryptionDecryption.Decryption(user.LoginPassword))
                {
                    return string.Empty;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Startup.SECRET);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.LoginUsername),
                    new Claim(ClaimTypes.Role, userRole.UserLoginType1),

                    }),

                    Expires = DateTime.UtcNow.AddMinutes(112),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user.Token;
            }
            catch(Exception ee)
            {
                return ee.ToString();
            }
        }
    }
}
