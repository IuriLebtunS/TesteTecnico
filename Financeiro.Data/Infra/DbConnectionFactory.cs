using System.Data.SqlClient;
using System.Configuration;

namespace Financeiro.Data.Infra
{
    public static class DbConnectionFactory
    {
        public static SqlConnection Create()
        {
            var cs = ConfigurationManager
                .ConnectionStrings["FinanceiroDb"]
                .ConnectionString;

            return new SqlConnection(cs);
        }
    }
}