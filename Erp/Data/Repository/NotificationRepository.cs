using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private AccountDbContext _context;
        private Management _management;

        public NotificationRepository(Management management, AccountDbContext accountDbContext)
        {
            _context = accountDbContext;
            _management = management;
        }

       

    }
}
