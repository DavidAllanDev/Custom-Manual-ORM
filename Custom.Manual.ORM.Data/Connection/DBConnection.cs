using System;
using System.Data.SqlClient;
using Custom.Manual.ORM.Data.Setting;

namespace Custom.Manual.ORM.Data.Connection
{
    public class DBConnection : IDBConnection
    {
        private SqlCommand _sqlCommand;
        private SqlConnection _sqlConnection;
        public IConnectionSetting Settings { get; private set; }

        public DBConnection(IConnectionSetting connectionSetting)
        {
            Settings = connectionSetting ?? throw new InvalidOperationException("");

            var connectionString = @"Data Source=" + Settings.Server + ";" +
                         "Initial Catalog=" + Settings.Database + ";" +
                         "User id=" + Settings.User + ";" +
                         "Password=" + Settings.Password + ";";

            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
            _sqlConnection.Close();
        }

        public SqlConnection Connection() => _sqlConnection;

        public SqlDataReader Result() => _sqlCommand.ExecuteReader();

        public void SetCommand(string sql)
        {
            _sqlCommand = new SqlCommand(sql, _sqlConnection);
            _sqlCommand.Connection.Open();
        }

        public object SetInsertCommand(string sql)
        {
            _sqlCommand = new SqlCommand(sql, _sqlConnection);
            return _sqlCommand.ExecuteScalar();
        }

        public int SetCountCommand(string sql)
        {
            return (int)SetInsertCommand(sql);
        }

        public bool SetUpdateCommand(string sql)
        {
            _sqlCommand = new SqlCommand(sql, _sqlConnection);
            return _sqlCommand.ExecuteNonQuery() != 0;
        }
    }
}
