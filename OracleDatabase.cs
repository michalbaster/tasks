using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Tasks
{
    public class OracleDatabase : IDatabase
    {
        private readonly IContextDbProvider _contextDbProvider;
        private OracleConnection _oracleConnection;

        public OracleDatabase(IContextDbProvider contextDbProvider)
        {
            _contextDbProvider = contextDbProvider;
        }
        
        public void Dispose()
        {
            _oracleConnection?.Close();
            _oracleConnection?.Dispose();
            _oracleConnection = null;
        }

        public IDbConnection GetConnection()
        {
            var dbDetails = _contextDbProvider.GetDb();
            string connectionString = $"User Id={dbDetails.Login};Password={dbDetails.Password};Data Source={dbDetails.Tns}";

            _oracleConnection = new OracleConnection(connectionString) {TnsAdmin = "tns"};
            _oracleConnection.Open();
            return _oracleConnection;
        }
    }
}