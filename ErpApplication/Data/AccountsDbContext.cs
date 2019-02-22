using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace ErpApplication.Data
{
    public class AccountsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> Admins { get; set; }

        public AccountsDbContext(DbContextOptions<AccountsDbContext> optionsBuilder) : base(optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ApplicationUser>(Builder =>
            //{
            //    Builder.HasIndex(e => e.DatabaseName).IsUnique();
            //});
            base.OnModelCreating(modelBuilder);
        }
    }
}
