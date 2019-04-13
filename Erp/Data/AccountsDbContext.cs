using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace Erp.Data
{
    public class AccountsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> ErpUsers { get; set; }

        public AccountsDbContext(DbContextOptions<AccountsDbContext> optionsBuilder) : base(optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {             
            base.OnModelCreating(modelBuilder);
        }
    }
}
