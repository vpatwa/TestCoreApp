using System.Data;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Persistence;

namespace TestCoreApp.Infrastructure.Domain
{
    public class RateChartRepository : IRateChartRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public RateChartRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        internal IDbConnection connection => _sqlConnectionFactory.GetOpenConnection();
    }
}
