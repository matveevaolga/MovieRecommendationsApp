using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class DBFunctions
    {
        DBConnection connectorToDb;
        MySqlDataAdapter cursor;
        ResourceManager rm;

        public DBFunctions()
        {
            connectorToDb = new DBConnection();
            cursor = new MySqlDataAdapter();
            rm = new ResourceManager("FormProject.Properties.Resources",
                typeof(DBFunctions).Assembly);
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
