using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Erp.ViewModels;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class NotificationApiController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private INotificationRepository _notificationRepo;
        private INotificationResponseRepository _notificationResponseRepo;
        private INotificationUserRepository _notificationUser;

        public NotificationApiController(INotificationRepository notificationRepo,
            UserManager<ApplicationUser> userManager,INotificationResponseRepository notificationResponseRepo, INotificationUserRepository notificationUser)
        {
            _userManager = userManager;
            _notificationRepo = notificationRepo;
            _notificationResponseRepo = notificationResponseRepo;
            _notificationUser = notificationUser;

        }
        [HttpPost("SendNotification")]
        public async Task<ActionResult<long>> SendNotification(NotificationViewModel notificationViewModel)
        {
            try
            {
               
                List<NotificationResponses> nres = new List<NotificationResponses>();
                List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
                List<NotificationApplicationUser> notificationUsers = new List<NotificationApplicationUser>();                           
                foreach (var item in notificationViewModel.UserNames)
                {
                    var user = await _userManager.FindByNameAsync(item);
                    if (user != null)
                    {
                        applicationUsers.Add(user);
                        notificationUsers.Add(new NotificationApplicationUser
                        {

                            ApplicationUserId = user.Id,

                        });
                    }
                    else
                    {
                        return BadRequest("UserName: " + item + " DoesNotExist");
                    }

                }
                foreach (var item in notificationViewModel.Responses)
                {
                    nres.Add(new NotificationResponses { Response = item });
                }
                var notification = new Notification
                {
                    message = notificationViewModel.Message,
                    NotificationType = notificationViewModel.NotificationType,
                };

                await _notificationRepo.Insert(notification);

                return Ok("Your Notification ID is : " + notification.Id);
            }
            catch (Exception)
            {
                return BadRequest("error");
                
            }


        }

       
        [HttpGet("GetNotifications")]
        [Authorize(AuthenticationSchemes = "Bearer, Identity.Application")]
        public async Task<ActionResult<List<Notification>>> GetNotifications(string UserId)
        {
            if (UserId == null)
            {

                var identity = (ClaimsIdentity)User.Identity;
                UserId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
              
                
            }
            var notifications = await _notificationRepo.GetUnResponsedNotifications(UserId);

            
            return Ok(notifications);
            
            

        }


    }
}