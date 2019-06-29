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

namespace Erp.Repository
{
    public class UserTaskRepository : Repository<UserTask, AccountDbContext>, IUserTaskRepository
    {
        private AccountDbContext _accountDbContext;
        private UserManager<ApplicationUser> _userManager;

        public UserTaskRepository(IConfiguration config, ILogger<UserTaskRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountDbContext = accountDbContext;
            _userManager = userManager;

        }

        public  async Task AssigneUserTask(UserTask ob)
        {
            string userid = await GetNonBusyUser(ob.ApplicationRoleId);
            ob.ApplicationUserId = userid;
            await base.Insert(ob);
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
            Console.WriteLine("user" + user.Id);
            //To-Do create Notification
            return user.Id;
        }

    }
}
