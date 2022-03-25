using BangOnERP.DataBase;
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
    [Route("StateMasterApi/")]
    [ApiController]
    public class StateController : Controller
    {
        private IState stateRepo;

        public StateController(IState _stateRepo)
        {
            stateRepo = _stateRepo;
        }

        // GET: StateController
        [Route("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(stateRepo.GetAll());
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }


        // GET: StateController/Details/5
        [Route("GetById")]
        [HttpGet]
        public IActionResult Get(string UserTypeId)
        {
            try
            {
                if (!String.IsNullOrEmpty(UserTypeId))
                {
                    return Ok(stateRepo.Edited(UserTypeId));
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

        // POST: StateController/Create
        [Route("StateProvider")]
        [HttpPost]
        public IActionResult Post(MasterStateCustomer state)
        {
            try
            {
                if (state != null)
                {
                    return Ok(stateRepo.Inserted(state));
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


        // POST: StateController/Edit/5
        [Route("StateService")]
        [HttpPut]
        public IActionResult Put(string stateId, MasterStateCustomer userState)
        {
            try
            {
                if (userState != null)
                {
                    userState.StateId = (EncryptionDecryption.Decryption(userState.StateId));
                    return Ok(stateRepo.Updated(userState));
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



        // POST: StateController/Delete/5
        [Route("StateDisposable")]
        [HttpDelete]
        public IActionResult Delete(string Desposables)
        {
            try
            {
                if (!String.IsNullOrEmpty(Desposables))
                {
                    return Ok(stateRepo.Deleted(Desposables));
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
