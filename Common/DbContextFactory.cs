using System;
using System.Data.Entity;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

// using MySql.Data.MySqlClient;

namespace EntrepreneurCommon.Common
{
    public enum DbConnectionType { SqLite, MySql };

    public static class DbContextFactory
    {
        [Obsolete("Obsoleted by the DatabaseEf class.")]
        public static T CreateDbContext<T>(DbConnectionType connectionType, string connectionString) where T : DbContext
        {
            switch (connectionType) {
                case DbConnectionType.MySql:
                    var mysqlConnection = new MySqlConnection(connectionString);
                    return (T)Activator.CreateInstance(typeof(T), new object[] { mysqlConnection, true });
                case DbConnectionType.SqLite:
                    var sqliteConnection = new SQLiteConnection(connectionString);
                    sqliteConnection.Open();
                    return (T)Activator.CreateInstance(typeof(T), new object[] { sqliteConnection, true });
                default:
                    return null;
            }
        }
    }
}
