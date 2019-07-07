using Erp.Data;
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
using Erp.Hubs;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Erp.Repository
{
    public class NotificationRepository : Repository<Notification, AccountDbContext> , INotificationRepository
    {
        private IHubContext<NotificationHub> _notificationHubContext;
        private AccountDbContext _context;
        private Management _management;

        public NotificationRepository(IConfiguration config, ILogger<NotificationRepository> ilogger, IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _notificationHubContext = hubContext;
            _context = accountDbContext;
            _management = management;
        }

        public async Task<List<Notification>> GetUnResponsedNotifications(string userId)
        {
            List<Notification> notifications = new List<Notification>();
            var result = _context.Notifications
                
                //.Include(x => x.Notification.NotificationResponses)
                .Where(dt => dt.ApplicationUserId == userId).ToList();
            notifications = result;
            return notifications;
        }
        public override async Task Insert(Notification notification)
        {
            await base.Insert(notification);
            await _notificationHubContext.Clients.User(notification.ApplicationUserId).SendAsync("recieveNotification", notification);
           
        }
    }
    
    public class NotificationResponseRepositroy : Repository<NotificationResponses, AccountDbContext>, INotificationResponseRepository
    {
        private AccountDbContext _context;
        private Management _management;

        public NotificationResponseRepositroy(IConfiguration config, ILogger<NotificationResponseRepositroy> ilogger, IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _context = accountDbContext;
            _management = management;
        }





    }
    
}
