using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class ProplemReporting : BackgroundService
    {
        private IConfiguration _config;
        private Emergency _emergency;
        private ILogger<ProplemReporting> _logger;

        public ProplemReporting(ILogger<ProplemReporting> logger,IConfiguration config, Emergency emergency, MailQueue mailQueue)
        {
            _config = config;
            _emergency = emergency;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Problem Reporting Service is Starting");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() => { checkErpDbConnection(); checkOrgDbConnection(); });
                Thread.Sleep(10000);
            }
            _logger.LogInformation("Problem Reporting Service is Stopping");
        }

        private void checkErpDbConnection()
        {
            using (var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    _emergency.sendEmergencyEmail(ex.Message);
                    
                }
                
            }
        }private void checkOrgDbConnection()
        {
            using (var conn = new MySqlConnection(_config.GetConnectionString("MySql")))
            {
                try
                {
                    conn.Open();
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("here");
                    _emergency.sendEmergencyEmail(ex.Message);
                    
                }
                
            }
        }
    }
}
