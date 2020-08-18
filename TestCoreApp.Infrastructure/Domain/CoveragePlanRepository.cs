using System.Data;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Persistence;

namespace TestCoreApp.Infrastructure.Domain
{
    public class CoveragePlanRepository : ICoveragePlanRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public CoveragePlanRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        internal IDbConnection connection => _sqlConnectionFactory.GetOpenConnection();
    }
}
