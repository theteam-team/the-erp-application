﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public  class Address
    {
        public uint id;
        public string city;
        public string governate;
        public string street;
        public uint zip_code;
        public string customer_id;
        public string Crm_employee_id;

    }
}
