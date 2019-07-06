using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Erp.Data.Entities;

namespace Erp.Data
{
    public class AccountDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ApplicationUser> ErpUsers { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<BpmWorker> BpmWorkers { get; set; }
        public DbSet<UserTaskParameters> UserTaskParameters { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<UserHasEmail> UserHasEmails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationApplicationUser> NotificationUsers { get; set; }
      
        public DbSet<ProcRequest> ProcRequests { get; set; }
        public DbSet<BpmWorkflowParameters> WorkflowParameters { get; set; }

        public AccountDbContext(DbContextOptions<AccountDbContext> optionsBuilder) : base(optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {             
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Organization>().HasIndex(org => org.Name).IsUnique();
            modelBuilder.Entity<UserHasEmail>().HasKey(sc => new { sc.EmailId, sc.EmailTypeId, sc.ApplicationUserId });
            modelBuilder.Entity<NotificationApplicationUser>().HasKey(sc => new { sc.NotificationId, sc.ApplicationUserId });
        }
    }
}
