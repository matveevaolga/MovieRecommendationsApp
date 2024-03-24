using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MovieRecommendationsApp.MVVM.ViewModel
{
    public class UserDataProcessing
    {
        public static string GetPasswordHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
