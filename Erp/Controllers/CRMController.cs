﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data;
using Erp.ModulesWrappers;
using Erp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Erp.Controllers
{
    /// <summary>
    /// Web Api To Contact The CRM modules Web Services
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CRM_APIController : ControllerBase
    {
        private DataDbContext _dataDbContext;  //object to connect with the Database Containing The Crm Data
        /// <summary>
        /// a constructror gets its services parameters resolved by the runtime from the service container
        /// </summary>
        /// <param name="dataDbContext"></param>
        public CRM_APIController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;

        }
        /// <summary>
        /// rest web service that responds to a post http request with a body containing an object of the Class numbers
        /// This web service Contact with a specific C++ Function in the CRM dll through a wrapper to serve the request
        /// </summary>
        /// <param name="numbers">The object passed To the web service </param>
        /// <returns>the summation of the fields of the passed object </returns>
        [HttpPost("AddNumber")]
        public ActionResult<int> AddNumbers(Numbers numbers)
        {
            return Crm_Wrapper.AddNumbers(numbers.N1, numbers.N2) ;
        }

        /// <summary>
        /// rest web service that responds to a post http request with a body containing an object of the Class numbers
        /// This web service Contact with a specific C++ Function in the CRM dll through a wrapper to serve the request
        /// </summary>
        /// <param name="numbers">The object passed To the web service </param>
        /// <returns>the multiplication of the fields of the passed object </returns>
        [HttpPost("MultiplyNumbers")]
        public ActionResult<int> MultiplyNumbers(Numbers numbers)
        {
            return Crm_Wrapper.MultiplyNumbers(numbers.N1, numbers.N1);
        }

        
        

        
    }
}