using System.Data;

namespace InsightFlow.Infrastructure.Interfaces;

public interface IDbConnectionPool
{
    IDbConnection GetConnection();

    void ReturnConnection(IDbConnection connection);
}