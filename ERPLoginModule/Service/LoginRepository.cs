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

        public async Task<List<LoginUser>> Deleted(string id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string idstr = EncryptionDecryption.Decryption(id);
                    LoginSuperLogin delUser = db.LoginSuperLogins.Find(Convert.ToInt32(idstr));
                    db.LoginSuperLogins.Remove(delUser);
                    await db.SaveChangesAsync();
                    transaction.Commit();

                    var attachments = mapper.Map<List<VwLoginLoginUser>, List<LoginUser>>(db.VwLoginLoginUsers.ToList());
                    return attachments;

                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<VwLoginLoginUser> Edited(string id)
        {
            try
            {
                string idstr= EncryptionDecryption.Decryption(id);
                if(Convert.ToInt32(idstr)<=0)
                {
                    return null;
                }
                var gh = await db.VwLoginLoginUsers.FirstOrDefaultAsync(x => x.LoginSuperLoginId == Convert.ToInt32(idstr));
                gh.LoginSuperLoginId = 0;
                //                gh.LoginPassword = EncryptionDecryption.Decryption(gh.LoginPassword);
                return gh;
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public async Task<List<LoginUser>> GetAll()
        {
            try
            {
                var attachments = mapper.Map<List<VwLoginLoginUser>,List<LoginUser>>(await db.VwLoginLoginUsers.ToListAsync());
                return attachments;
            }
            catch (Exception ee)
            {
                throw;
            }
        }

        public async Task<string> Inserted(LoginSuperLogin obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    obj.LoginPassword = EncryptionDecryption.Encryption(obj.LoginPassword);
                    obj.LoginCreatedDate = DateTime.Now;
                    db.LoginSuperLogins.Add(obj);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.LoginSuperLoginId.ToString());
                }
                catch (Exception ee)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<string> Updated(LoginUser obj)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    obj.LoginSuperLoginId = (EncryptionDecryption.Decryption(obj.LoginSuperLoginId.ToString()));
                    LoginSuperLogin getFrmId = db.LoginSuperLogins.AsNoTrackingWithIdentityResolution().
                    Single(m => m.LoginSuperLoginId ==Convert.ToInt32(obj.LoginSuperLoginId));

                    getFrmId.LoginUpdatedDate = DateTime.Now;
                    getFrmId.LoginUsername = obj.LoginUsername;
                    getFrmId.LoginActive = obj.LoginActive;
                    db.LoginSuperLogins.Update(getFrmId);
                    await db.SaveChangesAsync();
                    transaction.Commit();
                    return EncryptionDecryption.Encryption(obj.LoginSuperLoginId.ToString());
                }

                catch (Exception ee)
                {
                    transaction.Rollback();
					throw;
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
				throw;
            }
        }
    }
}
