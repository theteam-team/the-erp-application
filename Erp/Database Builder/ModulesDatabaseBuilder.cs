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

        public void createModulesDatabase(string name)
        {
            string sqlCode;
            string path = GetTablesPath();
            _logger.LogInformation(path);
            using (var fileStream = new FileStream(Path.GetFullPath(Path.Combine(path, @"Erp.sql")), FileMode.Open, FileAccess.ReadWrite))
            {
                BinaryReader br = new BinaryReader(fileStream);
                byte[] filByte = br.ReadBytes((int)fileStream.Length);
                string mnSql = Encoding.ASCII.GetString(filByte);
                //var stream = new FileStream(Path.Combine(path, @"\"+name+".sql"), FileMode.Open, FileAccess.ReadWrite);
                sqlCode = mnSql.Replace("ERP", name);
                File.WriteAllText(Path.GetFullPath(Path.Combine(path, @"" + name + ".sql")), sqlCode);
                

            }
            runSqlCode(sqlCode);
        }


        public void createNewModule(string databaseName, string moduleName)
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
            runSqlCode(newDatabase);
            File.WriteAllText(databasePath, newDatabase);

        }

        public void runSqlCode(string sqlCode)
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("MySql_Local")))
            {
                conn.Open();
                var cmd = new MySqlCommand(sqlCode, conn);
                _logger.LogInformation("Creating the Database");
                cmd.ExecuteNonQuery();
                _logger.LogInformation("Database Created");

            }
        }
        public string GetTablesPath()
        {
            string path = _env.ContentRootPath;
            path = Path.GetFullPath(Path.Combine(path, @"..\"));
            path = Path.GetFullPath(Path.Combine(path, @"DatabaseTables"));
            return path;
        
        }
    }

}
