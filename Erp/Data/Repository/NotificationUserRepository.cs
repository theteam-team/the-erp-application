using Erp.Data;
using Erp.Data.Entities;
using Erp.Hubs;
using Erp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class NotificationUserRepository : Repository<NotificationApplicationUser, AccountDbContext>, INotificationUserRepository
    {
        private IHubContext<NotificationHub> _notificationHubContext;
        private AccountDbContext _context;
        private Management _management;

        public NotificationUserRepository(IConfiguration config, ILogger<NotificationUserRepository> ilogger, IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor ,management, datadbContext, accountDbContext, userManager)
        {
            _notificationHubContext = hubContext;
            _context = accountDbContext;
            _management = management;
        }
        public async Task<List<string>> GetUsersInNotification(long notificationId)
        {
            List<string> users = new List<string>();
            var notifications = _context.NotificationUsers.Where(dt => dt.NotificationId == notificationId);
            foreach (var item in notifications)
            {
                users.Add(item.ApplicationUserId);
            }
            return users;
        }
        public async Task<NotificationApplicationUser> GetById(string userId, long NotificationId)
        {
            var result = _context.NotificationUsers
                .Where(dt => dt.ApplicationUserId == userId && dt.NotificationId == NotificationId).FirstOrDefault();
            Console.WriteLine(userId);
            return result;
        }
       
       

    }
}
