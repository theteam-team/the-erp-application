using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;


namespace Erp.Database_Builder
{
    public class ModulesDatabaseBuilder
    {
        private ILogger<ModulesDatabaseBuilder> _logger;
        private IHostingEnvironment _env;
        private IConfiguration _configuration;

        public ModulesDatabaseBuilder(IConfiguration configuration, IHostingEnvironment env, ILogger<ModulesDatabaseBuilder> logger)
        {
            _logger = logger;
            _env = env;
            _configuration = configuration;

        }

        public async Task<bool> createModulesDatabaseAsync(string name)
        {
            string sqlCode;
            string path = GetTablesPath();
            string mainDbpath = Path.Combine(path, @"Main System");
            string companyPath = Path.Combine(path, @"Companies/" + name);
            bool databaseCreated = false;
            _logger.LogInformation(path);
            using (var fileStream = new FileStream(Path.Combine(mainDbpath, @"Erp.sql"), FileMode.Open, FileAccess.ReadWrite))
            {
                BinaryReader br = new BinaryReader(fileStream);
                byte[] filByte = br.ReadBytes((int)fileStream.Length);
                string mnSql = Encoding.ASCII.GetString(filByte);
              
                sqlCode = mnSql.Replace("ERP", name);
                databaseCreated = await runSqlCodeAsync(sqlCode);
                if (databaseCreated) {
                    Directory.CreateDirectory(companyPath);
                    File.WriteAllText(Path.GetFullPath(Path.Combine(companyPath, @"" + name + ".sql")), sqlCode);
                }

            }
            return databaseCreated;
        }


        public async Task<bool> createNewModule(string databaseName, string moduleName)
        {
            string path = GetTablesPath();
            _logger.LogInformation(path);
            string databasePath = Path.Combine(path, databaseName + ".sql");
            _logger.LogInformation(databasePath);
            string newModulePath = Path.Combine(path, moduleName + ".sql");
            _logger.LogInformation(newModulePath);

            string databaseSql = File.ReadAllText(databasePath);
            string moduleSql = File.ReadAllText(newModulePath);
            string newDatabase = string.Concat(databaseSql, moduleSql);
            File.WriteAllText(databasePath, newDatabase);
            return await runSqlCodeAsync(newDatabase);

        }

        public async Task<bool> runSqlCodeAsync(string sqlCode)
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("MySql")))
            {

                try
                {
                    conn.Open();
                    var cmd = new MySqlCommand(sqlCode, conn);
                    _logger.LogInformation("Creating the Database");
                    await Task.Run(() => { cmd.ExecuteNonQuery(); });
                    _logger.LogInformation("Database Created");
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Failed To create The database due to error" + ex.Message);
                    return false;
                }

            }
        }
        public string GetTablesPath()
        {
            string path = _env.ContentRootPath;
            path = Path.GetFullPath(Path.Combine(path, @"..\"));
            path = Path.GetFullPath(Path.Combine(path, @"Database Tables"));
            return path;
        
        }
    }

}
