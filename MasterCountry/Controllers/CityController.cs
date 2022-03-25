using BangOnERP.GlobalEncryptionDecryption;
using MasterLocation.Business.Contexctual;
using MasterLocation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterLocation.Controllers
{
    [Route("CityMasterApi/")]
    [ApiController]
    public class CityController : Controller
    {
        private ICity cityRepo;

        public CityController(ICity _cityRepo)
        {
            cityRepo = _cityRepo;
        }


        // GET: CityController
        [Route("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
          
                return Ok(cityRepo.GetAll());
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

        [Route("GetById")]
        [HttpGet]
        public IActionResult Get(string cityId)
        {
            try
            {
                if (!String.IsNullOrEmpty(cityId))
                {
                    return Ok(cityRepo.Edited(cityId));
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
        [Route("GetCountry")]
        [HttpGet]
        public IActionResult GetCountry()
        {
            try
            {
                return Ok(cityRepo.GetAllCountry());
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET: CityController/Edit/5
        [Route("GetByCountry")]
        [HttpGet]
        public IActionResult GetState(string CountryId)
        {
            try
            {
                if (!String.IsNullOrEmpty(CountryId))
                {
                    return Ok(cityRepo.GetStatesByCountry(CountryId));
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

        [Route("CityProvider")]
        [HttpPost]
        public IActionResult Post(MasterCityCustomer city)
        {
            try
            {
                if (city != null)
                {
                    return Ok(cityRepo.Inserted(city));
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

        [Route("CityService")]
        [HttpPut]
        public IActionResult Put(MasterCityCustomer userCity)
        {
            try
            {
                if (userCity != null)
                {
                    userCity.CityId = (EncryptionDecryption.Decryption(userCity.CityId));
                    return Ok(cityRepo.Updated(userCity));
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

        [Route("CityDisposable")]
        [HttpDelete]
        public IActionResult Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(cityRepo.Deleted(Desposables));
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
