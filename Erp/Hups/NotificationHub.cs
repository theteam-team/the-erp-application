using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Interfaces;
using Erp.Data;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Erp.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly Management _management;
        private readonly INotificationUserRepository _notificationUserRepository;

        public NotificationHub(Management management, INotificationUserRepository notificationUserRepository)
        {
            _management = management;
            _notificationUserRepository = notificationUserRepository;
        }
        public async Task sendNotification(Notification notification, string userId)
        {
            
            await Clients.User(userId).SendAsync("receiveNotification", notification);
        }
        public async Task sendUserTask(string userId ,UserTask userTask)
        {
            
            await Clients.User(userId).SendAsync("recieveUserTask", userTask);
        }

       
        public async Task removeUserTask(string UserTaskid)
        {
            Console.WriteLine("removeUserTask");
            string userId = ((ClaimsIdentity)Context.User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            await Clients.Group(UserTaskid).SendAsync("removeUserTask", UserTaskid);
        }

        public async Task AddToGroupRole()
        {
            IList<string> userRole = await _management.GetUserRoleAsync(Context.User);
            foreach (var el in userRole)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, el);
            }

        }
        public async Task AddToGroup(string groupName)
        { 
           
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }



    }
}
