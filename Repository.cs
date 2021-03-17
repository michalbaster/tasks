using Dapper;

namespace Tasks
{
    public class Repository : IRepository
    {
        private readonly IDatabase _database;

        public Repository(IDatabase database)
        {
            _database = database;
        }
        
        public string GetDbName()
        {
            using var dbConnection = _database.GetConnection();
            var dbname = dbConnection.QuerySingle<string>(@"select name from V$database");
            return dbname;
        }
    }
}