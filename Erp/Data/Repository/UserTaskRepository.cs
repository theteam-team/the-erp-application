using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data;
using Erp.Interfaces;
using Erp.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Erp.Hubs;

namespace Erp.Repository
{
    public class UserTaskRepository : Repository<UserTask, AccountDbContext>, IUserTaskRepository
    {
        private INotificationRepository _notificationRepository;
        private AccountDbContext _accountDbContext;
        private UserManager<ApplicationUser> _userManager;
        private IHubContext<NotificationHub> _hubContext;

        public UserTaskRepository(INotificationRepository notificationRepository, IConfiguration config, ILogger<UserTaskRepository> ilogger, IHubContext<NotificationHub> hubContext, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _notificationRepository = notificationRepository;
            _accountDbContext = accountDbContext;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public  async Task AssigneUserTask(UserTask ob)
        {
            string userid = await GetNonBusyUser(ob.ApplicationRoleId);
            ob.ApplicationUserId = userid;
            await base.Insert(ob);
            await _hubContext.Clients.User(userid).SendAsync("recieveUserTask", ob);
            Notification notification = new Notification()
            {
                message = "You have been Assigned this Task " + ob.Title,
                NotificationType = "userTask",
                EntityID = ob.Id,
                ApplicationUserId = userid

            };
            await _notificationRepository.Insert(notification);
            //To-Do create Notification
        }
        public async Task<string> GetNonBusyUser(string RoleId)
        {
            string databaseName = _ConnectionString.DATABASE;

            List<ApplicationUser>  result =
                 (
                  from ur in _accountDbContext.UserRoles where  ur.RoleId == RoleId
                  join u in _accountDbContext.Users on ur.UserId equals u.Id
                  where u.DatabaseName == databaseName
                  select u)
                  .Include(dt => dt.UserTasks).ToList();

            var user = result.FirstOrDefault();
            //To-Do create Notification
            return user.Id;
        }

        public async Task<List<UserTask>> GetAssignedUserTask(string userId)
        {
            List<UserTask> userTasks = _accountDbContext.UserTasks
                .Include(x=>x.UserTaskParameters)
                .Where(u => u.ApplicationUserId == userId && !u.IsDone).ToList();
            return userTasks;
        }
        public override async Task<UserTask> GetById(object userId)
        {
            UserTask userTask = _accountDbContext.UserTasks
                .Include(x=>x.UserTaskParameters)
                .Where(u => u.Id == (string)userId).FirstOrDefault();
            return userTask;
        }

    }
}
