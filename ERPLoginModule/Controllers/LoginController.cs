using BangOnERP.DataBase;
using ERPLoginModule.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPLoginModule.Service.Contextual;
using BangOnERP.GlobalEncryptionDecryption;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPLoginModule.Controllers
{
   [Route("Loginapi/")]
   
    [ApiController]
    public class LoginController : Controller
    {
        private LoginI loginRepo;
        private readonly IMapper _mapper;

        public LoginController(LoginI _loginRepo)// IMapper mapper)
        {
            loginRepo = _loginRepo;
//            _mapper = mapper;

        }

        // GET: api/<LoginController>


        [Route("GetAll")]
        //[Authorize]
        //[Authorize(Roles="Admin")]
        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
               
                return Ok( loginRepo.GetAll());
            }
            catch(Exception ee)
            {
                 return BadRequest(ee.Message);
            }
        }

        // GET api/<LoginController>/5
        [Route("GetById")]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get(string LoginSuperLoginId)
        {
            try
            {
                if(!String.IsNullOrEmpty(LoginSuperLoginId))
                {
                  //  var userViewModel = _mapper.Map<List<LoginUser>>(loginRepo.Edited(LoginSuperLoginId));
                    return Ok(loginRepo.Edited(LoginSuperLoginId));
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

        // POST api/<LoginController>
        [Route("LoginProvider")]
        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post(LoginSuperLogin supLogin)
        {
            try { 
            if(supLogin!= null)
                {
                    return Ok(loginRepo.Inserted(supLogin));
                }
                else
                {
                    return BadRequest("Invalid Data Supplied");
                }
            }
            catch(Exception ee)
            {
                return BadRequest(ee.ToString());
            }
        }

        // PUT api/<LoginController>/5
        [Route("LoginService")]
        [Authorize]
        [HttpPut]
        public IActionResult Put(LoginUser supLogin)
        {
            try
            {
                if (supLogin != null)
                {
                    supLogin.LoginSuperLoginId = (EncryptionDecryption.Decryption(supLogin.LoginSuperLoginId.ToString()));
                    return Ok(loginRepo.Updated(supLogin));
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
        [Route("LoginDisposable")]
        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(loginRepo.Deleted(Desposables));
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
        [Route("GetPosted")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginSuperLogin user)
        {
            var token = loginRepo.Login(user.LoginUsername, user.LoginPassword);

            if (token == null || token == String.Empty)
                return BadRequest(new { message = "User name or password is incorrect" });

            return Ok(token);
        }
    }
}
