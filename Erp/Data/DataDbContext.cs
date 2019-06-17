using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Erp.Data.Entities;


namespace Erp.Data
{
   
    /// <summary>
    /// this class works as an interface to the database containing the data of the modules
    /// </summary>

    public class DataDbContext : DbContext 
    {
        /// This proberty represents a table in the database to the Modules Entity and can be used to make a CRUD operation on this table 
        //public DbSet<WorkflowInstance> WorkflowInstances { get; set; }
        public DbSet<Modules> Modules  { get; set; }
        public DbSet<NodeLangWorkflow> NodeLangWorkflow { get; set; }
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserHasEmail>().HasKey(sc => new { sc.EmailId, sc.EmailTypeId, sc.ApplicationUserId });
        }

        /*public DbSet<Email> Emails { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<UserHasEmail> UserHasEmails { get; set; }*/
        /// this  proberty used to set the connection string to the database
        /// 

        //public DataDbContext(DbContextOptions<DataDbContext> optionsBuilder) : base(optionsBuilder) { }
        public string ConnectionString { get; set; }

        /// ovrriding this method from the base class and pass to it an optionBuilder Containing the New _config to the database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

        }
        

    }
}
