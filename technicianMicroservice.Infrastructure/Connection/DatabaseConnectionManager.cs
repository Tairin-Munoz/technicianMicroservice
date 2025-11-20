// technicianMicroservice.Infrastructure/Connection/DatabaseConnectionManager.cs
using System;

namespace technicianMicroservice.Infrastructure.Connection
{
    public sealed class DatabaseConnectionManager
    {
        private static DatabaseConnectionManager? _instance;
        private static readonly object _locker = new();
        public string ConnectionString { get; private set; }

        private DatabaseConnectionManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // First-time initialization with the connection string
        public static DatabaseConnectionManager GetInstance(string connectionString)
        {
            if (_instance is null)
            {
                lock (_locker)
                {
                    _instance ??= new DatabaseConnectionManager(connectionString);
                }
            }
            return _instance;
        }

        // Subsequent calls (after initialized)
        public static DatabaseConnectionManager GetInstance()
        {
            return _instance ?? throw new InvalidOperationException(
                "Database connection manager not initialized. Call GetInstance(connectionString) first.");
        }

        public void UpdateConnectionString(string connectionString)
        {
            lock (_locker)
            {
                ConnectionString = connectionString;
            }
        }
    }
}
