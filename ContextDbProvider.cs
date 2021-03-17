using System.Threading;

namespace Tasks
{
    public class ContextDbProvider : IContextDbProvider
    {
        AsyncLocal<DbDetails> _dbDetails = new AsyncLocal<DbDetails>(); 
        
        public void SetDb(DbDetails dbDetails)
        {
            _dbDetails.Value = dbDetails;
        }

        public DbDetails GetDb()
        {
            return _dbDetails.Value;
        }
    }
}