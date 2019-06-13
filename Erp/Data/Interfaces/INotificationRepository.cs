using Erp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface INotificationRepository : IRepository<Notification,AccountDbContext>
    {
       
    }
    public interface INotificationUserRepository : IRepository<NotificationApplicationUser,AccountDbContext>
    {
         Task<NotificationApplicationUser> GetById(string userId, long NotificationId);


    }
    public interface INotificationResponseRepository : IRepository<NotificationResponses,AccountDbContext>
    {
       
    }
}
