using MySqlConnector;
namespace AvaloniaApplication2;

public class DBHelper
{
    public MySqlConnectionStringBuilder _connectionString { get; }

    public DBHelper()
    {
        _connectionString = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "proapple19",
            UserID = "root",
            Password = "123456"

        };
    }
}