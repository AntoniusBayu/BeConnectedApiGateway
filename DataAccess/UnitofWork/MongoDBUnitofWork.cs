namespace DataAccess
{
    public class MongoDBUnitofWork : IUnitofWork
    {
        public MongoDBConnection _MongoDBConn { get; set; }

        public MongoDBUnitofWork(IConnection _conn)
        {
            _MongoDBConn = (MongoDBConnection)_conn;
        }
        public void BeginTransaction()
        {
            _MongoDBConn._session.StartTransaction();
        }

        public void CommitTransaction()
        {
            _MongoDBConn._session.CommitTransaction();
        }

        public void Dispose()
        {
            _MongoDBConn.Dispose();
        }

        public string GetAppSettings(string key)
        {
            return _MongoDBConn.GetAppSettings(key);
        }

        public void OpenConnection(string connString, string dbName = "")
        {
            _MongoDBConn.OpenConnection(connString, dbName);
        }

        public void RollbackTransaction()
        {
            _MongoDBConn._session.AbortTransaction();
        }
    }
}
