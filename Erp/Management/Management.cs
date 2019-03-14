using Erp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp
{
    public class Management
    {
        private RoleManager<ApplicationRole> mroleManager;
        private DataDbContext mdataDbContext;
        private AccountsDbContext mContext;
        private UserManager<ApplicationUser> mUserManager;
        private SignInManager<ApplicationUser> mSignInManager;

        public Management(AccountsDbContext context, DataDbContext dataDbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
           )
        {
            
            mroleManager = roleManager;
            mdataDbContext = dataDbContext;
            mContext = context;
            mUserManager = userManager;
            
        }

        public async Task AddRoleToUser(string roleName, ApplicationUser user)
        {

            await CreateRoleAsync(roleName);

            await mUserManager.AddToRoleAsync(user, roleName);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var role = await mroleManager.RoleExistsAsync(roleName);
            if (!role)
            {
                await mroleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }

        public async Task<IList<string>> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await mUserManager.GetRolesAsync(user);
            return roles;
        }
        public async Task<IList<string>> GetUserRoleAsync(System.Security.Claims.ClaimsPrincipal user)
        {
            var _user= await mUserManager.GetUserAsync(user);
           return await mUserManager.GetRolesAsync(_user);
        }

        public bool checkRole(System.Security.Claims.ClaimsPrincipal user, string roleName)
        {
            return user.IsInRole(roleName);
        }

    }
}
