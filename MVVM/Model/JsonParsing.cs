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

        public static ServerData ParseServer()
        {
            string json = System.IO.File.ReadAllText("..\\..\\..\\Datas\\Access\\ServerData.json");
            ServerData serverData = JsonSerializer.Deserialize<ServerData>(json);
            return serverData;
        }
    }
}
