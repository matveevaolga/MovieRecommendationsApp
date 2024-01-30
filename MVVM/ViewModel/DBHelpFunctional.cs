using MovieRecommendationsApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationsApp.MVVM.ViewModel
{
    public class DBHelpFunctional
    {
        public static bool LoginExists(string login)
        {
            DBFunctions dBFunctions = new DBFunctions();
            return dBFunctions.LoginExists(login);
        }

        public static bool PasswordCorrect(string login, string password)
        {
            DBFunctions dBFunctions = new DBFunctions();
            password = UserDataProcessing.GetPasswordHash(password);
            bool check = dBFunctions.PasswordCorrect(login, password);
            return check;
        }

        public static void Register(string login, string password, string email)
        {
            password = UserDataProcessing.GetPasswordHash(password);
            DBFunctions dBFunctions = new DBFunctions();
            int profileId = dBFunctions.AddToProfilesTable(login, password, email);
            if (profileId == 0) { dBFunctions.DeleteProflile(profileId); return; }
            try { dBFunctions.AddToUsersTable(login, password, email, profileId); }
            catch (Exception ex) 
                { Console.WriteLine(ex.GetType().Name); dBFunctions.DeleteProflile(profileId); }
        }
    }
}
