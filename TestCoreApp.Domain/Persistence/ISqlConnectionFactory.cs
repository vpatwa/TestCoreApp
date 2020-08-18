using System.Data;

namespace TestCoreApp.Domain.Persistence
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
