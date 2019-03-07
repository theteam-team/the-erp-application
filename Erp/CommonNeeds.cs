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
    public static class CommonNeeds
    {
        public static object myclass { get; set; }
        public static Dictionary<System.Security.Claims.ClaimsPrincipal, string> CurrentPath
        = new Dictionary<System.Security.Claims.ClaimsPrincipal, string>();

        public static bool checkdtb(DataDbContext dataDbContext ,string Database)
        {
            dataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database="
                    + Database + ";Trusted_Connection=True;MultipleActiveResultSets=true";
            return (dataDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }


        public static void loginLogger()
        {
   
        }
    }
}
