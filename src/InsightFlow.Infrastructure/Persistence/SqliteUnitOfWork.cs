using InsightFlow.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence;

public class SqliteUnitOfWork : UnitOfWork
{
    public SqliteUnitOfWork(DbContextOptions dbContextOptions, IDbConnectionPool dbConnectionPool) : base(dbContextOptions, dbConnectionPool)
    {
    }
}