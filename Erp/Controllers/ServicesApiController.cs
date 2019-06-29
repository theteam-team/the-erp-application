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
using System.Security.Claims;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ServicesApiController : ControllerBase
    {
        public  Management _management { get; }
        public  IUserTaskRepository _userTaskRepository { get; }
        public  UserManager<ApplicationUser> _userManager { get; }
        public  IEmailRepository _email { get; }
        public  IEmailTypeRepository _emailType { get; }
        public  IEmailUserRepository _emailUser { get; }
        public  DataDbContext _dataDbContext { get; }
        public  AccountDbContext _accountDbContext { get; }
        public  ILogger<ServicesApiController> _logger { get; }


        public ServicesApiController(Management management, IUserTaskRepository userTaskRepository, IEmailUserRepository emailUser, IEmailRepository email, IEmailTypeRepository emailType,
           UserManager<ApplicationUser> userManager, ILoggerFactory loggerFactory)
        {
            _management = management;
            _userTaskRepository = userTaskRepository;
            _userManager = userManager;
            _email = email;
            _emailType = emailType;
            _emailUser = emailUser;
            
            //TaskQueue = taskQueue;
            _logger = loggerFactory.CreateLogger<ServicesApiController>();
        }

        [HttpPost("AssignUserTask")]
        public async Task<ActionResult> AssignUserTask([FromBody] UserTaskViewModel userTaskView)
        {
            Console.WriteLine("Assigne " + userTaskView.IsBpmEngine);
            if (!userTaskView.IsBpmEngine)
            {
                userTaskView.InvokerId = ((ClaimsIdentity) HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            string roleId = await _management.getRoleIdAsync(userTaskView.RoleName);
            var userTask = new UserTask
            {
                ApplicationRoleId = roleId,
                UserTaskParameters = userTaskView.UserTaskParameters,
                Title = userTaskView.Title,
                InvokerID = userTaskView.InvokerId
                
            };
            await _userTaskRepository.AssigneUserTask(userTask);
            return Ok();
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

           
        }

        [HttpPost("AddEmailTypes")]
        public async Task<ActionResult<string>> AddEmailTypes([FromBody]List<EmailType> emailTypes)
        {
            foreach (var item in emailTypes)
            {
                await _emailType.Insert(new EmailType { Id = item.Id, Type = item.Type });
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