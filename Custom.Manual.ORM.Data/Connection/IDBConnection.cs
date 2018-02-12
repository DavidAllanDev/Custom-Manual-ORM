using System.Data.SqlClient;
using Custom.Manual.ORM.Data.Setting;

namespace Custom.Manual.ORM.Data.Connection
{
    public interface IDBConnection
    {
        IConnectionSetting Settings { get; }
        SqlConnection Connection();
        void SetCommand(string sql);
        object SetInsertCommand(string sql);
        int SetCountCommand(string sql);
        bool SetUpdateCommand(string sql);
        SqlDataReader Result();
    }
}
