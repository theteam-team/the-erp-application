using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Data
{
    public class Test
    {
        public Test(IHttpContextAccessor contextAccessor)
        {
            Console.Write(contextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
