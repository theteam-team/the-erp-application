using Erp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Erp
{
    /// <summary>
    /// Contains Common Properties and Methods needed For each http request
    /// </summary>
    public class CommonNeeds
    {

        /// <summary>
        /// Used To Check if a database with this specifc name is existed or not
        /// </summary>
        /// <param name="dataDbContext">The Database Context used to manage the database connection</param>
        /// <param name="DatabaseName">the Database Name</param>
        /// <returns></returns>
        /*public static bool checkdtb(DataDbContext dataDbContext ,string DatabaseName)
        {
            dataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database="
                    + DatabaseName + ";Trusted_Connection=True;MultipleActiveResultSets=true";
            return (dataDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }*/
     
    }
}
