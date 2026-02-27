using System;
using System.Data.SqlClient;

namespace Financeiro.Data.Infra
{
    public static class DbConnectionFactory
    {
        private static string _connectionString;

        public static void Configure(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static SqlConnection Create()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception("ConnectionString não configurada. Chame DbConnectionFactory.Configure().");

            return new SqlConnection(_connectionString);
        }
    }
}