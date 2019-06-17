using Erp.Data;
using Erp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IEmailUserRepository : IRepository<UserHasEmail, AccountDbContext>
    {
        Task<List<UserHasEmail>> GetUnSentMails();
    }
    public interface IEmailRepository : IRepository<Email, AccountDbContext>
    {

    }
    public interface IEmailTypeRepository : IRepository<EmailType, AccountDbContext>
    {

    }
}
