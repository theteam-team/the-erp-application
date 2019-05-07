using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Hups
{
    public class NotificationHub : Hub
    {
        private readonly Management _management;

        public NotificationHub(Management management)
        {
            _management = management;
        }
        public async Task sendNotification(string UserRole, string notificationId, string message)
        {
            await Clients.Group(UserRole).SendAsync("receiveNotification", notificationId, message);
        }

        public async Task notificationResponse(string notificationId, bool response)
        {
            Console.WriteLine(notificationId + " " + response);
            await Clients.Group("Engine").SendAsync("notificationResponse", notificationId, response);
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
