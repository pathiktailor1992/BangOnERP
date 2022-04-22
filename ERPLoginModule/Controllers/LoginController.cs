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

       // public LoginController() { }

        public LoginController(LoginI _loginRepo)// IMapper mapper)
        {
            loginRepo = _loginRepo;
        }

        // GET: api/<LoginController>


        [Route("GetAll")]
        [Authorize]
        [Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result =await loginRepo.GetAll();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok( result);
            }
            catch(Exception ee)
            {
                 return BadRequest(ee.Message);
            }
        }

        // GET api/<LoginController>/5
        [Route("GetById")]
        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get(string LoginSuperLoginId)
        {
            try
            {
                if(!String.IsNullOrEmpty(LoginSuperLoginId) && LoginSuperLoginId!="0")
                {

                    VwLoginLoginUser kh =await loginRepo.Edited(LoginSuperLoginId).ConfigureAwait(false);
                    if (kh == null)
                    {
                        return NotFound("Invalid Data Supplied");
                    }
                    
                    return Ok(kh);
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
        public async Task<IActionResult>  Post(LoginSuperLogin supLogin)
        {
            try { 
            if(supLogin!= null && ModelState.IsValid)
                {
                    var result = await loginRepo.Inserted(supLogin);
                    if (result != null)
                    {
                        return Ok(result);

                    }
                    else
                    {
                        return BadRequest("Invalid Data Supplied");
                    }
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
        public async Task<IActionResult> Put(LoginUser supLogin)
        {
            try
            {
                if (supLogin != null && ModelState.IsValid)
                {
                    return Ok(await loginRepo.Updated(supLogin));
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
        public async Task<IActionResult> Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(await loginRepo.Deleted(Desposables));
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
