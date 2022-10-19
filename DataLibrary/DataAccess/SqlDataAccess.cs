using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        // I pull this connectionName so I don't have to identify it for common scenarios
        public static string GetConnectionString(string connectionName = "MVCDemoDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        // SQL Connection grabs the data from the DB using the conn string established above
        /* <T> Explanation:
         * Load data, and here's the model I want you you to load into <T>. This returns a List of that model.
         * I connect to SQL and say, 'Give me this string to call (string sql here). Execute that SQL and load
         * that query into type <T>. This query returns an <I> enumerable, but I want a List. Thus, I use .ToList()
         * to return that whole thing. That's all we need to load data if we DON'T need parameters.
         * */
        public static List<T> LoadData<T>(string sql)
        {
            // Get conn string from ln 16 and open it here in SqlConnection()
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        // Looking in object 'data' for FirstName property, take that value, and replace it.
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }
    }
}
