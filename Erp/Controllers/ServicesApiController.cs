using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.Data;
using Erp.ViewModels;
using Erp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ServicesApiController : ControllerBase
    {
        private readonly IEmailRepository _email;
        private readonly IEmailTypeRepository _emailType;
        private readonly IEmailUserRepository _emailUser;
        private readonly DataDbContext _dataDbContext;
        private readonly AccountDbContext _accountDbContext;
        private readonly ILogger<ServicesApiController> _logger;
      

        public ServicesApiController(IEmailUserRepository emailUser, IEmailRepository email, IEmailTypeRepository emailType,
            ILoggerFactory loggerFactory, DataDbContext datadbcontext, AccountDbContext accountDbContext)
        {
            _email = email;
            _emailType = emailType;
            _emailUser = emailUser;
            _dataDbContext = datadbcontext;
            if (!CommonNeeds.checkdtb(_dataDbContext, "Admin"))
            {
                _dataDbContext.Database.EnsureCreated();
            }
            _accountDbContext = accountDbContext;
            //TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<ServicesApiController>();
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult<string>> SendEmail([FromBody] EmailEntity emailEntity)
        {
            Email email = new Email()
            {
                Id = emailEntity.Email.Id,
                Subject = emailEntity.Email.Subject,
                Message = emailEntity.Email.Message,

            };
            EmailType emailType = new EmailType()
            {
                Id = emailEntity.EmailType.Id,
                Type = emailEntity.EmailType.Type
            };
            string userId = emailEntity.UserId;
            try
            {

              
               if (await _email.GetById(email.Id) == null)
                {
                    _accountDbContext.Emails.Add(email);

                }
                if (await _emailType.GetById(emailType.Id) == null)
                {
                    
                    _accountDbContext.EmailTypes.Add(emailType);

                }

                _accountDbContext.SaveChanges();
                
                var user = _accountDbContext.ErpUsers.Where(dt => dt.Id == userId).FirstOrDefault();
                var emailUser = new UserHasEmail()
                {
                    EmailId = email.Id,
                    //Email = e,
                    ApplicationUserId = userId,
                    //ApplicationUser = user,
                    EmailTypeId = emailType.Id,
                   // EmailType = eT


                };
                _accountDbContext.UserHasEmails.Add(emailUser);
                _accountDbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}