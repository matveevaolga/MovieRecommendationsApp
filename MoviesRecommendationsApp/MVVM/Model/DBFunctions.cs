using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class DBFunctions
    {
        DBConnection connectorToDb;
        MySqlDataAdapter cursor;

        public DBFunctions()
        {
            connectorToDb = new DBConnection();
            cursor = new MySqlDataAdapter();
        }

        public static void CreateLocalDB()
        {
            string sript = File.ReadAllText(@"../../../Datas/Access/CreateLocalDBScript.txt");
            DBConnection connection = new DBConnection(false);
            connection.OpenConnection();
            MySqlCommand createDB = new MySqlCommand(sript, connection.GetConnection());
            createDB.ExecuteNonQuery();
        }

        public bool LoginExists(string login)
        {
            DataTable table = new DataTable();
            connectorToDb.OpenConnection();
            MySqlCommand registered = new MySqlCommand("select * from users " +
                "where login = @uLogin;", connectorToDb.GetConnection());
            registered.Parameters.AddWithValue("@uLogin", login);
            cursor.SelectCommand = registered;
            cursor.Fill(table);
            connectorToDb.CloseConnection();
            return table.Rows.Count > 0;
        }

        public bool EmailExists(string email)
        {
            DataTable table = new DataTable();
            connectorToDb.OpenConnection();
            MySqlCommand registered = new MySqlCommand("select * from users " +
                "where email = @uEmail;", connectorToDb.GetConnection());
            registered.Parameters.AddWithValue("@uEmail", email);
            cursor.SelectCommand = registered;
            cursor.Fill(table);
            connectorToDb.CloseConnection();
            return table.Rows.Count > 0;
        }

        public bool PasswordCorrect(string login, string password)
        {
            DataTable table = new DataTable();
            connectorToDb.OpenConnection();
            MySqlCommand registered = new MySqlCommand("select password from users " +
                "where login = @uLogin limit 1;", connectorToDb.GetConnection());
            registered.Parameters.AddWithValue("@uLogin", login);
            object storedPass = registered.ExecuteScalar();
            connectorToDb.CloseConnection();
            return storedPass.ToString() == password;
        }

        public int AddToProfilesTable(string login, string password, string email)
        {
            connectorToDb.OpenConnection();
            MySqlCommand idProfileInsert = new MySqlCommand
                ("insert into profiles (name) values ('Пользователь');", connectorToDb.GetConnection());
            idProfileInsert.ExecuteNonQuery();
            MySqlCommand getCurrentID = new MySqlCommand
                ("select last_insert_id() from profiles;", connectorToDb.GetConnection());
            object currentID = getCurrentID.ExecuteScalar();
            if (currentID == null) { return 0; }
            connectorToDb.CloseConnection();
            return Convert.ToInt32(currentID.ToString());
        }

        public void DeleteProflile(int profileId)
        {
            connectorToDb.OpenConnection();
            MySqlCommand idProfileInsert = new MySqlCommand
                ($"delete from profiles where idProfile={profileId};", connectorToDb.GetConnection());
            idProfileInsert.ExecuteNonQuery();
            connectorToDb.CloseConnection();
        }

        public void AddToUsersTable(string login, string password,
            string email, int profileId)
        {

            connectorToDb.OpenConnection();
            string insertLogPass = "insert into users (login, password, email, idProfile) " +
                "values (@uLogin, @uPass, @uEmail, @uProfileId);";
            MySqlCommand register = new MySqlCommand(insertLogPass, connectorToDb.GetConnection());
            register.Parameters.AddWithValue("@uLogin", login);
            register.Parameters.AddWithValue("@uPass", password);
            register.Parameters.AddWithValue("@uEmail", email);
            register.Parameters.AddWithValue("@uProfileId", profileId);
            register.ExecuteNonQuery();
            connectorToDb.CloseConnection();
        }

        public int GetUserIdByLogin(string login)
        {
            connectorToDb.OpenConnection();
            string getUserId = "select idUser from users where login = @uLogin limit 1;";
            MySqlCommand register = new MySqlCommand(getUserId, connectorToDb.GetConnection());
            register.Parameters.AddWithValue("@uLogin", login);
            int id = (int)register.ExecuteScalar();
            connectorToDb.CloseConnection();
            return id;
        }

        public void AddPreference(int userId, int movieId)
        {
            connectorToDb.OpenConnection();
            string insertPreference = "insert into preferencesinfo (idUser, movieId) values" +
                " (@userId, @idMovie);";
            MySqlCommand register = new MySqlCommand(insertPreference, connectorToDb.GetConnection());
            register.Parameters.AddWithValue("@userId", userId);
            register.Parameters.AddWithValue("@idMovie", movieId);
            register.ExecuteNonQuery();
            connectorToDb.CloseConnection();
        }

        public void DeletePreference(int userId, int movieId)
        {
            connectorToDb.OpenConnection();
            string insertPreference = "delete from preferencesinfo where idUser=@userId and movieId=@idMovie;";
            MySqlCommand register = new MySqlCommand(insertPreference, connectorToDb.GetConnection());
            register.Parameters.AddWithValue("@userId", userId);
            register.Parameters.AddWithValue("@idMovie", movieId);
            register.ExecuteNonQuery();
            connectorToDb.CloseConnection();
        }

        public List<int> GetUserPreferences(string login)
        {
            connectorToDb.OpenConnection();
            string getPreferences = "select movieId from preferencesinfo inner" +
                                    " join users on users.idUser = preferencesinfo.idUser" +
                                    " where users.login = @uLogin;";
            MySqlCommand register = new MySqlCommand(getPreferences, connectorToDb.GetConnection());
            register.Parameters.AddWithValue("@uLogin", login);
            List<int> preferences = new List<int>();
            MySqlDataReader dr = register.ExecuteReader();
            while (dr.Read())
            { preferences.Add(dr.GetInt32(0)); }
            connectorToDb.CloseConnection();
            return preferences;
        }

        public void UpdateDaysOnline(string login)
        {
            connectorToDb.OpenConnection();
            string command = "update profiles inner join users on " +
                            "users.idProfile = profiles.idProfile " +
                            "set profiles.onlineDays = profiles.onlineDays + 1 " +
                            "where users.login = @uLogin;";
            MySqlCommand updateDays = new MySqlCommand
                (command, connectorToDb.GetConnection());
            updateDays.Parameters.AddWithValue("@uLogin", login);
            updateDays.ExecuteNonQuery();
            connectorToDb.CloseConnection();
        }

        public DateTime? GetLastDayOnline(string login)
        {
            connectorToDb.OpenConnection();
            string command = "select lastDayOnline from profiles inner join users on " +
                            "users.idProfile = profiles.idProfile " +
                            "where users.login = @uLogin limit 1;";
            MySqlCommand getLastDay = new MySqlCommand
                (command, connectorToDb.GetConnection());
            getLastDay.Parameters.AddWithValue("@uLogin", login);
            DateTime? lastDay = getLastDay.ExecuteScalar() as DateTime?;
            connectorToDb.CloseConnection();
            return lastDay;
        }
    }
}
