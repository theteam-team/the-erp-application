using Erp.Data;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Erp
{
    /// <summary>
    /// This class represents the management System which is used To Assign users roles, get users roles ... etc
    /// </summary>
    public class Management
    {
        private RoleManager<ApplicationRole> mroleManager; //object For Managing A roles stored In a datastore "ex : Database"
        private UserManager<ApplicationUser> mUserManager; //Object Used For Manageing Users In Stored In a datastore "ex : Database"
        
        /// this constructor parameters will be assigned using the dependancy injection
        public Management(AccountDbContext context, DataDbContext dataDbContext,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            mroleManager = roleManager;
            mUserManager = userManager;
            
        }

        /// <summary>
        /// Add A Role to A specifc user. 
        /// This is an Async Mesthod and returns a Task so you must await it.
        /// </summary>
        /// <param name="roleName">The name of the Role</param>
        /// <param name="user">The specifc User To which the role is assigned </param>
        /// <returns>Task</returns>
        
        public async Task AddRoleToUserAsync(string roleName, ApplicationUser user)
        {
            await CreateRoleAsync(roleName);
            
            await mUserManager.AddToRoleAsync(user, roleName);
        }
        public async Task<string> getRoleIdAsync(string roleName)
        {
            return await mroleManager.GetRoleIdAsync(await mroleManager.FindByNameAsync(roleName));
        }

        /// <summary>
        /// Create A specfic Role
        /// if the role existed , no action is taken if not , the role will be created 
        /// This is an Async Mesthod and returns a Task so you must await it.
        /// </summary>
        /// <param name="roleName">The name of the Role to be created</param>
        /// <returns>Task</returns>
        public async Task CreateRoleAsync(string roleName)
        {
            var role = await mroleManager.RoleExistsAsync(roleName);
            if (!role)
            {
                await mroleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
        /// <summary>
        /// Returns a List of the roles assigned To a specific User
        /// </summary>
        /// <param name="user">The User we wish to see its assigned roles</param>
        /// <returns>The list of the Assigned roles</returns>

        public async Task<IList<string>> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await mUserManager.GetRolesAsync(user);
            return roles;
        }
        /// <summary>
        /// Returns a List of the roles assigned To a specific User
        /// </summary>
        /// <param name="user">The User we wish to see its assigned roles, this user represent the securety claims in the current HttpContext</param>
        /// <returns></returns>
        public async Task<IList<string>> GetUserRoleAsync(System.Security.Claims.ClaimsPrincipal user)
        {
            var _user= await mUserManager.GetUserAsync(user);
           return await mUserManager.GetRolesAsync(_user);
        }
        /// <summary>
        /// check if a user is in the specific Role or not
        /// </summary>
        /// <param name="user">this user represent the securety claims in the current HttpContext</param>
        /// <returns>bool</returns>
        public bool checkRole(System.Security.Claims.ClaimsPrincipal user, string roleName)
        {
            
            return user.IsInRole(roleName);
        }

    }
}
