using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace ErpApplication.Data
{
    public class AccountsDbContext : IdentityDbContext<AdminsTable>
    {
        public DbSet<AdminsTable> Admins { get; set; }

        public AccountsDbContext(DbContextOptions<AccountsDbContext> optionsBuilder) : base(optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
