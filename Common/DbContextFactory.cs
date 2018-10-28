using System;
using System.Data.Entity;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace EntrepreneurCommon.Common
{
    public enum DbConnectionType { SqLite, MySql };

    public static class DbContextFactory
    {

        public static T CreateDbContext<T>(DbConnectionType connectionType, string connectionString) where T : DbContext
        {
            switch (connectionType) {
                case DbConnectionType.MySql:
                    var mysql_connection = new MySqlConnection(connectionString);
                    return (T)Activator.CreateInstance(typeof(T), new object[] { mysql_connection, true });
                case DbConnectionType.SqLite:
                    var sqlite_connection = new SQLiteConnection(connectionString);
                    sqlite_connection.Open();
                    return (T)Activator.CreateInstance(typeof(T), new object[] { sqlite_connection, true });
                default:
                    return null;
            }
        }
    }
}
