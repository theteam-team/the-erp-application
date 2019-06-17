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
using Microsoft.AspNetCore.Identity;
using Erp.Data.Entities;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ServicesApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailRepository _email;
        private readonly IEmailTypeRepository _emailType;
        private readonly IEmailUserRepository _emailUser;
        private readonly DataDbContext _dataDbContext;
        private readonly AccountDbContext _accountDbContext;
        private readonly ILogger<ServicesApiController> _logger;


        public ServicesApiController(IEmailUserRepository emailUser, IEmailRepository email, IEmailTypeRepository emailType,
           UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _email = email;
            _emailType = emailType;
            _emailUser = emailUser;
            
            //TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<ServicesApiController>();
        }

        [HttpPost("SendEmail")]
        public async Task<ActionResult<string>> SendEmail([FromBody] EmailViewModel emailEntity)
        {
            List<UserHasEmail> UserEmails = new List<UserHasEmail>();
            Email email = new Email()
            {
                Id = emailEntity.Email.Id,
                Subject = emailEntity.Email.Subject,
                Message = emailEntity.Email.Message,

            };

            foreach (var item in emailEntity.UserNames)
            {
                var user = await _userManager.FindByNameAsync(item);
                if (user != null)
                {
                    UserEmails.Add(new UserHasEmail { EmailTypeId = emailEntity.EmailTypeId, ApplicationUserId = user.Id });
                }
                else
                {
                    return BadRequest("UserName: " + item + " Does Not Exist");
                }
            }
            email.UserHasEmails = UserEmails;
            _email.Insert(email);
            return Ok();

            //try
            //{

            //    //var emailUser = new UserHasEmail()
            //    //{
            //    //    EmailId = email.Id,
            //    //    //Email = e,
            //    //    ApplicationUserId = userId,
            //    //    //ApplicationUser = user,
            //    //    EmailTypeId = emailType.Id,
            //    //   // EmailType = eT


            //    //};

            //    //_accountDbContext.UserHasEmails.Add(emailUser);
            //    //_accountDbContext.SaveChanges();

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost("AddEmailTypes")]
        public async Task<ActionResult<string>> AddEmailTypes([FromBody]List<EmailType> emailTypes)
        {
            foreach (var item in emailTypes)
            {
                _emailType.Insert(new EmailType { Id = item.Id, Type = item.Type });
            }
            return Ok();
        }
        [HttpGet("GetEmailType")]
        public async Task<ActionResult<List<EmailType>>> GetEmailType()
        {
           
            return Ok(await _emailType.GetAll());
        }


    }
}