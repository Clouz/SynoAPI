﻿using System;
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

            var xxx = syno.DownloadStation.Tasks.List(server);
            
            var logout = syno.API.Auth.GetLogout(server);


            Console.ReadLine();
        }
    }
}