using Erp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Erp
{
    public static class CommonNeeds
    {
        public static object myclass { get; set; }
        public static Dictionary<System.Security.Claims.ClaimsPrincipal, string> CurrentPath
        = new Dictionary<System.Security.Claims.ClaimsPrincipal, string>();

        public static void checkdtb(DataDbContext dataDbContext ,string Database)
        {
            dataDbContext.ConnectionString = "Server=(localdb)\\ProjectsV13;Database="
                    + Database + ";Trusted_Connection=True;MultipleActiveResultSets=true";
            dataDbContext.Database.EnsureCreated();
        }


        public static void loginLogger()
        {
   
        }
    }
}
