using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Erp.ViewModels;
using Erp.Data;
using Erp.Interfaces;
namespace Erp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class NotificationApiController : ControllerBase
    {
        private INotificationRepository _notificationRepo;
        private INotificationResponseRepository _notificationResponseRepo;
        private INotificationUserRepository _notificationUser;

        public NotificationApiController(INotificationRepository notificationRepo,
            INotificationResponseRepository notificationResponseRepo, INotificationUserRepository notificationUser)
        {
            _notificationRepo = notificationRepo;
            _notificationResponseRepo = notificationResponseRepo;
            _notificationUser = notificationUser;

        }
        [HttpPost("AddNotification")]
        public async Task<ActionResult<long>> AddNotification(NotificationViewModel notificationViewModel)
        {
            try
            {

                List<NotificationResponses> nres = new List<NotificationResponses>();
                foreach (var item in notificationViewModel.Responses)
                {
                    nres.Add(new NotificationResponses { Response = item });
                }
                var notification = new Notification
                {
                    message = notificationViewModel.Message,
                    NotificationResponses = nres,

                };
                var notificationUser = new NotificationApplicationUser
                {
                    Notification = notification,
                    ApplicationUserId = notificationViewModel.UserID,

                };
                await _notificationUser.Insert(notificationUser);
                return Ok("Your Notification ID is : " + notification.Id + notificationViewModel.UserID);
            }
            catch (Exception)
            {
                return BadRequest("error");
                throw;
            }


        }

        [HttpGet("GetNotificationResponse/{UserId}/{notificationId}")]
        public async Task<ActionResult<string>> GetNotificationResponse([FromRoute]string UserId, [FromRoute] long notificationId)
        {
            var notificationUser = await _notificationUser.GetById(UserId, notificationId);

            if (notificationUser.IsResponsed)
            {
                return Ok(notificationUser.Response);
            }
            else
            {
                return BadRequest("wait");
            }

        }
    }
}