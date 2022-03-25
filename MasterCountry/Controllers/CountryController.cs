using BangOnERP.GlobalEncryptionDecryption;
using MasterLocation.Business.Contexctual;
using MasterLocation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCountry.Controllers
{
    [Route("CountryMasterApi/")]
    [ApiController]
    public class CountryController : Controller
    {
        private ICountry countryRepo;

        public CountryController(ICountry _countryRepo)
        {
            countryRepo = _countryRepo;
        }
        // GET: CountryController
        // GET: api/<UserRoleController>

        [Route("GetAll")]
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(countryRepo.GetAll());
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
                    return Ok(countryRepo.Edited(UserTypeId));
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
        [Route("CountryProvider")]
        [HttpPost]
        public IActionResult Post(BangOnERP.DataBase.MasterCountry country)
        {
            try
            {
                if (country != null)
                {
                    return Ok(countryRepo.Inserted(country));
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
        [Route("CountryService")]
        [HttpPut]
        public IActionResult Put(MasterCountryCustomer userCountry)
        {
            try
            {
                if (userCountry != null)
                {
                    userCountry.CountryId = (EncryptionDecryption.Decryption(userCountry.CountryId.ToString()));
                    return Ok(countryRepo.Updated(userCountry));
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
        [Route("CountryDisposable")]
        [HttpDelete]
        public IActionResult Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(countryRepo.Deleted(Desposables));
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
