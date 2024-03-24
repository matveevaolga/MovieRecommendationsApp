using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class DBConnection
    {
        MySqlConnection connection;
        string connectionString = "server={0};port={1};username={2};password={3};database={4}";
        string connectionWithoutDB = "server={0};port={1};username={2};password={3}";

        public DBConnection(bool isDataBaseCreated=true)
        {
            JsonParsing.ServerData serverData = JsonParsing.ParseServer();
            if (isDataBaseCreated)
            {
                connection = new MySqlConnection(string.Format(connectionString, serverData.Server,
                    serverData.Port, serverData.Username, serverData.Password, serverData.Database));
            }
            else
            {
                connection = new MySqlConnection(string.Format(connectionWithoutDB,
                    serverData.Server, serverData.Port, serverData.Username, serverData.Password));
            }
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) { connection.Open(); }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
