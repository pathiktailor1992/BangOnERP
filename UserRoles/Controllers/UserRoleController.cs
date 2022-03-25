using BangOnERP.DataBase;
using BangOnERP.GlobalEncryptionDecryption;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoles.Business.Contexctual;
using UserRoles.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRoles.Controllers
{
    [Route("UserRoleApi/")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUserType userTypeRepo;

        public UserRoleController(IUserType _userTypeRepo)
        {
            userTypeRepo = _userTypeRepo;
        }

        // GET: api/<UserRoleController>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(userTypeRepo.GetAll());
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // GET api/<UserRoleController>/5
        [Route("GetById")]
        [HttpGet]
        public IActionResult Get(string UserTypeId)
        {
            try
            {
                if (!String.IsNullOrEmpty(UserTypeId))
                {
                    return Ok(userTypeRepo.Edited(UserTypeId));
                }
                else
                {
                    return BadRequest("Invalid Data Supplied");
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        // POST api/<UserRoleController>
        [Route("UserTypeProvider")]
        [HttpPost]
        public IActionResult Post(UserLoginType userLoginType)
        {
            try
            {
                if (userLoginType != null)
                {
                    return Ok(userTypeRepo.Inserted(userLoginType));
                }
                else
                {
                    return BadRequest("Invalid Data Supplied");
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.ToString());
            }
        }

        // PUT api/<UserRoleController>/5
        [Route("UserTypeService")]
        [HttpPut]
        public IActionResult Put(UserLoginTypeCustomer userType)
        {
            try
            {
                if (userType != null)
                {
                    userType.UserLoginTypeId = (EncryptionDecryption.Decryption(userType.UserLoginTypeId.ToString()));
                    return Ok(userTypeRepo.Updated(userType));
                }
                else
                {
                    return BadRequest("Invalid Data Supplied");
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.ToString());
            }
        }

        // DELETE api/<LoginController>/5
        [Route("UserTypeDisposable")]
        [HttpDelete]
        public IActionResult Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(userTypeRepo.Deleted(Desposables));
                }
                else
                {
                    return BadRequest("Invalid Data Supplied");
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }
    }
}
