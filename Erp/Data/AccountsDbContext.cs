using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace Erp.Data
{
    public class AccountDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> ErpUsers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<UserHasEmail> UserHasEmails { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> optionsBuilder) : base(optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {             
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserHasEmail>().HasKey(sc => new { sc.EmailId, sc.EmailTypeId, sc.ApplicationUserId });
        }
    }
}
