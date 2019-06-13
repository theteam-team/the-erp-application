using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.ViewModels
{
    public class _Email
    {
        [Key]
        public string Id { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }


    }
}
