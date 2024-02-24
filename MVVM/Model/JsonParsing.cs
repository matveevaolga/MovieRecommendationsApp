using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class JsonParsing
    {
        public class ServerData
        {
            public string Server { get; set; }
            public string Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Database { get; set; }
        }

        public class EmailData
        {
            public string Server { get; set; }
            public int Port { get; set; }
            public string SenderUsername { get; set; }
            public string SenderPassword { get; set; }
        }

        public static ServerData ParseServer()
        {
            string json = System.IO.File.ReadAllText("..\\..\\..\\Datas\\Access\\ServerData.json");
            ServerData? serverData = JsonSerializer.Deserialize<ServerData>(json);
            return serverData;
        }

        public static EmailData ParseEmail()
        {
            string json = System.IO.File.ReadAllText("..\\..\\..\\Datas\\Access\\EmailData.json");
            EmailData? emailData = JsonSerializer.Deserialize<EmailData>(json);
            return emailData;
        }
    }
}
