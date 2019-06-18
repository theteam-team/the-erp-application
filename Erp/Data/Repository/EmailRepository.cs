using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class EmailRepository :Repository<Email, AccountDbContext>, IEmailRepository
    {
        public EmailRepository(IConfiguration config, ILogger<EmailRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }
    }
    public class EmailUserRepository :Repository<UserHasEmail, AccountDbContext>, IEmailUserRepository
    {
        private AccountDbContext _accountDbContext;

        public EmailUserRepository(IConfiguration config, ILogger<EmailUserRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
             : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<List<UserHasEmail>> GetUnSentMails()
        {
            //InitiateConnection();
            List<UserHasEmail> userHasEmails = null;

            await Task.Run(()=> { userHasEmails = _accountDbContext.UserHasEmails.Where(u => u.IsSent == false).ToList(); });

            return userHasEmails;
        }
    }
    public class EmailTypeRepository :Repository<EmailType, AccountDbContext>, IEmailTypeRepository
    {
        public EmailTypeRepository(IConfiguration config, ILogger<EmailTypeRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
              : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}
