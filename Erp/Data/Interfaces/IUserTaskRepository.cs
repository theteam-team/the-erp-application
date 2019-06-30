using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Models;
using Erp.Data.Entities;
using Erp.Data;

namespace Erp.Interfaces
{
    public interface IUserTaskRepository : IRepository<UserTask, AccountDbContext>
    {
        Task AssigneUserTask(UserTask ob);
        Task<string> GetNonBusyUser(string RoleId);
        Task<List<UserTask>> GetAssignedUserTask(string userId);
    }
}
