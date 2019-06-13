using Erp.Data;

using Erp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class NotificationRepository : Repository<Notification, AccountDbContext> , INotificationRepository
    {
        private AccountDbContext _context;
        private Management _management;

        public NotificationRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {
            _context = accountDbContext;
            _management = management;
        }

       

    }
    public class NotificationUserRepository : Repository<NotificationApplicationUser, AccountDbContext>, INotificationUserRepository
    {
        private AccountDbContext _context;
        private Management _management;

        public NotificationUserRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {
            _context = accountDbContext;
            _management = management;
        }
        public async  Task<NotificationApplicationUser> GetById(string userId, long NotificationId)
        {
            var result = _context.NotificationUsers          
                .Where( dt => dt.ApplicationUserId == userId && dt.NotificationId == NotificationId).FirstOrDefault();
            Console.WriteLine(userId);
            return result;
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
