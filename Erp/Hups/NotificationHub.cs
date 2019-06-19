﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Interfaces;
using Erp.Data;
using Erp.Data.Entities;

namespace Erp.Hubs
{
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

        public async Task notificationResponse(long notificationId, string response)
        {
            List<Task> tasks = new List<Task>();
            //Console.WriteLine(notificationId + " " + response);
            List<string> users = await _notificationUserRepository.GetUsersInNotification(notificationId);
            
            tasks.Add( _notificationUserRepository.RespondToNotification(notificationId, response));
            foreach (var item in users)
            {
                Console.WriteLine(item);
                tasks.Add(Clients.User(item).SendAsync("removeNotification", notificationId));
            }
            await Task.WhenAll(tasks);

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
