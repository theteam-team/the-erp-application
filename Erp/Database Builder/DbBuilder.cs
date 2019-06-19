using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Erp.DatabaseBuilder
{
    public class DbBuilder
    {
        public DbBuilder()
        {

        }

        string createTable(string tableName, string databaseName, string sqlCode, MySqlConnection conn)
        {

            conn.Open();
            return null;
        }
        string createDatabase(string databaseName, string sqlCode, MySqlConnection conn)
        {

            conn.Open();
            return null;
        }


    }
}
