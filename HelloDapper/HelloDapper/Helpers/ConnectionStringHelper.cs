using System;
using System.Configuration;

namespace HelloDapper.Helpers
{
    public static class ConnectionStringHelper
    {
        private static string _connectionString;

        public static string GetConnectionString()
        {
            if (_connectionString != null) return _connectionString;

            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks2008R2_Data.mdf_Dapper"].ConnectionString;
            }
            catch (Exception exception)
            {
                throw new Exception("Could not get Connection string \"AdventureWorks2008R2_Data.mdf_Dapper\" from configuration.", exception);
            }

            return _connectionString;
        }
    }
}
