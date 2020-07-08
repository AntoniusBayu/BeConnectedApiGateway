using System;

namespace DataAccess
{
    public interface IConnection : IDisposable
    {
        void OpenConnection(string connString, string dbName = "");
        string GetAppSettings(string key);
    }
}
