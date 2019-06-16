﻿using Erp.Data;
using Erp.Hups;
using Erp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;

namespace Erp.Repository
{
    public class NotificationRepository : Repository<Notification, AccountDbContext> , INotificationRepository
    {
        private IHubContext<NotificationHub> _notificationHubContext;
        private AccountDbContext _context;
        private Management _management;

        public NotificationRepository(IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {
            _notificationHubContext = hubContext;
            _context = accountDbContext;
            _management = management;
        }

        public async Task<List<Notification>> GetUnResponsedNotifications(string userId)
        {
            List<Notification> notifications = new List<Notification>();
            var result = _context.NotificationUsers
                .Include(x => x.Notification)
                .Include(x => x.Notification.NotificationResponses)
                .Where(dt => dt.ApplicationUserId == userId && !dt.IsResponsed).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    notifications.Add(item.Notification);
                }
            }
            //Console.WriteLine(userId);
            return notifications;
        }
        public override async Task Insert(Notification notification)
        {
            await base.Insert(notification);
            List<Task> tasks = new List<Task>();
            foreach (var item in notification.NotificationApplicationUsers)
            {
                tasks.Add(_notificationHubContext.Clients.User(item.ApplicationUserId).SendAsync("receiveNotification", notification));

            }
           await Task.WhenAll(tasks);
        }
    }
    
    public class NotificationResponseRepositroy : Repository<NotificationResponses, AccountDbContext>, INotificationResponseRepository
    {
        private AccountDbContext _context;
        private Management _management;

        public NotificationResponseRepositroy(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {
            _context = accountDbContext;
            _management = management;
        }





    }
    
}
