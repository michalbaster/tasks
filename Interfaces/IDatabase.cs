using System;
using System.Data;

namespace Tasks
{
    public interface IDatabase : IDisposable
    {
        IDbConnection GetConnection();
    }
}