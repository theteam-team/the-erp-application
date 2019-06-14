using Erp.Hups;
using Erp.Interfaces;
using Erp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data
{
    public class NotificationUserRepository : Repository<NotificationApplicationUser, AccountDbContext>, INotificationUserRepository
    {
        private IHubContext<NotificationHub> _notificationHubContext;
        private AccountDbContext _context;
        private Management _management;

        public NotificationUserRepository(IHubContext<NotificationHub> hubContext, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(management, datadbContext, accountDbContext, userManager)
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
        public async Task<NotificationApplicationUser> GetResponse(long NotificationId)
        {
            var result = _context.NotificationUsers
                .Where(dt => dt.NotificationId == NotificationId && dt.IsResponsed).FirstOrDefault();
            //Console.WriteLine(userId);
            return result;
        }
        public async Task RespondToNotification(long NotificationId, string response)
        {
            List<Task> tasks = new List<Task>();
            var check = _context.NotificationUsers
                .Where(dt => dt.NotificationId == NotificationId && dt.IsResponsed).FirstOrDefault();
            if (check == null)
            {
                var result = _context.NotificationUsers
                    .Where(dt => dt.NotificationId == NotificationId && !dt.IsResponsed).ToList();
                foreach (var item in result)
                {
                    item.IsResponsed = true;
                    item.Response = response;
                    tasks.Add(base.Update(item));
                }
                await Task.WhenAll(tasks);
            }
            //Console.WriteLine(userId);
        }

    }
}
