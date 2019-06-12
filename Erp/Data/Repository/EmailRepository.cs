using Erp.Data;
using Erp.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class EmailRepository :Repository<Email, AccountDbContext>, IEmailRepository
    {
        public EmailRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
    }
    public class EmailUserRepository :Repository<UserHasEmail, AccountDbContext>, IEmailUserRepository
    {
        private AccountDbContext _accountDbContext;

        public EmailUserRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<List<UserHasEmail>> GetUnSentMails()
        {
            InitiateConnection();
            List<UserHasEmail> userHasEmails = null;

            await Task.Run(()=> { userHasEmails = _accountDbContext.UserHasEmails.Where(u => u.IsSent == false).ToList(); });

            return userHasEmails;
        }
    }
    public class EmailTypeRepository :Repository<EmailType, AccountDbContext>, IEmailTypeRepository
    {
        public EmailTypeRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
    }
}
