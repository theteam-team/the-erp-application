﻿using Erp.Data;
using Erp.Data.Entities;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer, DataDbContext>
    {
    }
}
