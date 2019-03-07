using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Data
{
   


    public class DataDbContext : DbContext 
    {
        public DbSet<Modules> Modules  { get; set; }
        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MyClassBuilder MCB = new MyClassBuilder("Student");
            //var myclass = MCB.CreateObject(new string[3] { "ID", "Name", "Address" },
            //    new Type[3] { typeof(int), typeof(string), typeof(string) });
            //CommonNeeds.myclass = myclass;
            //Type type = myclass.GetType();
           
            //var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });
            //if (type.IsClass)
            //{
            //    entityMethod.MakeGenericMethod(type)
            //        .Invoke(modelBuilder, new object[] { });
            //}
            base.OnModelCreating(modelBuilder);

            
        }

    }
}
