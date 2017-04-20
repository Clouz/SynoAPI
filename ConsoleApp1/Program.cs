using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;

using syno;
using System.Reflection;

namespace Test
{
    class Program
    {
        static HttpClient client;
        static CredentialCache cache;

        static string IP = "192.168.0.26";
        static string port = "5000";
        static string username = "Media";
        static string password = "f5_W@H";
        static Uri  baseAddress = new Uri($"http://{IP}:{port}");

        static void Main(string[] args)
        {

            Init server = new Init("http", "192.168.0.26", 5000, "Media", "f5_W@H");

            //login
            var login = syno.API.Auth.GetLogin(server);
            Console.WriteLine($"Login: {login.sid}");
            

            var xx = syno.DownloadStation.Info.GetConfig(server);

            var xxx = syno.DownloadStation.Tasks.Create(server, uri :"http://releases.ubuntu.com/17.04/ubuntu-17.04-server-amd64.iso.torrent?_ga=1.142332340.741759227.1492636896", destination : "download");

            Console.WriteLine(xxx);
            
            var logout = syno.API.Auth.GetLogout(server);


            Console.ReadLine();
        }

        public static void stampa(syno.DownloadStation.ListObjects lista)
        {
            Console.WriteLine($"Offset: {lista.offset}");
            Console.WriteLine($"Total: {lista.total}");

            foreach (var item in lista.tasks)
            {
                Console.WriteLine($"\t{item.id}:{item.title}");
            }
        }
    }
}
