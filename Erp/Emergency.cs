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
using System.Collections.Concurrent;
using System.Threading;
using Erp.BackgroundServices;
using Erp.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace Erp
{
    /// <summary>
    /// Contains Common Properties and Methods needed For each http request
    /// </summary>
    public class Emergency
    {
        private MailQueue _mailQueue;
        private IConfiguration _config;

        public Emergency(MailQueue mailQueue, IConfiguration config)
        {
            _mailQueue = mailQueue;
            _config = config;
        }

        public void sendEmergencyEmail(string Error)
        {
            Email email = new Email()
            {
                Subject = "Emergency Call",
                Message = "This An Emergency email Sent By the Problem Reporting service due to this " + Error + " Please Respond immediately",
                recieverEmail = _config["EmergencyContact:Email"],
                recieverName = _config["EmergencyContact:Name"]
            };

            _mailQueue.QueueExection(email);
        }

    }
}
