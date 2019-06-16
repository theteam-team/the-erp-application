using Erp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;

namespace Erp.Interfaces
{
    public interface INotificationRepository : IRepository<Notification,AccountDbContext>
    {
        Task<List<Notification>> GetUnResponsedNotifications(string userId);
    }
    public interface INotificationUserRepository : IRepository<NotificationApplicationUser,AccountDbContext>
    {
         Task<NotificationApplicationUser> GetById(string userId, long NotificationId);
         Task<NotificationApplicationUser> GetResponse(long NotificationId);
         Task RespondToNotification(long NotificationId, string response);
        Task<List<string>> GetUsersInNotification(long notificationId);

    }
    public interface INotificationResponseRepository : IRepository<NotificationResponses,AccountDbContext>
    {
       
    }
}
