using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class MongoDBConnection : IConnection
    {
        public IMongoClient _client;
        public IMongoDatabase _database;
        public IClientSession _session;
        private IConfiguration _config { get; set; }
        private IConfigurationSection _appsettings { get; set; }

        public MongoDBConnection(IConfiguration config)
        {
            _config = config;
        }
        public void Dispose()
        {
            this._session.Dispose();
        }

        public void OpenConnection(string connString, string dbName = "")
        {
            this._client = new MongoClient(connString);
            this._database = _client.GetDatabase(dbName);
            this._session = _client.StartSession();
        }

        public string GetAppSettings(string key)
        {
            this._appsettings = _config.GetSection("AppSettings");
            return _appsettings.GetSection(key).Value;
        }
    }
}
