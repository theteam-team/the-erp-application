using Erp.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class UserTaskResponseViewModel
    {
        public string id { get; set; }
       
        public List<ResponseParameterViewModel> userTaskParameters { get; set; }
    }
    
    public class ResponseParameterViewModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; } 
    }
}
