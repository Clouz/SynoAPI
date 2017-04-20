using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using System.Reflection;


namespace syno
{
    public class Init
    {
        static HttpClient client = new HttpClient();


        public string Scheme { get; set; } = "http";
        public string Host { get; set; } = "192.168.0.26";
        public int Port { get; set; } = 5000;
        public string Username { get; set; } = "Media";
        public string Password { get; set; } = "f5_W@H";
        public Uri BaseAddress { get; set; }

        public Init(string scheme, string host, int port, string username, string password)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.BaseAddress = new Uri($"{Scheme}://{Host}:{Port}/");
        }

        public static async Task<string> Richiesta(Uri richiesta)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, richiesta);
            HttpResponseMessage response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var risposta = await response.Content.ReadAsByteArrayAsync();
            string json = Encoding.UTF8.GetString(risposta, 0, risposta.Length);

            return json;
        }

        public static string GetParameters<T>(T Class)
        {
            Type ClassType = Class.GetType();
            PropertyInfo[] properties = ClassType.GetProperties();

            string result = "";

            foreach (PropertyInfo property in properties)
            {
                string name = property.Name;
                string value = property.GetValue(Class, null)?.ToString();

                if (value!=null)
                    result += $"&{name}={value}";
                //Console.WriteLine($"Prop name: {property.Name} ; Value: {property.GetValue(Class, null)}");
            }
            return result;
        }


    }


    [Serializable()]
    public class SynoException : Exception
    {
        public class errorObject
        {
            public int code { get; set; }
        }

        public enum ExceptionType
        {
            API_Auth,
            API_Info,
            DownloadStation_Info,
            DownloadStation_Schedule,
            DownloadStation_Statistic,
            DownloadStation_Task,
        }

        private static Dictionary<string, string> errorTable = new Dictionary<string, string>()
            {
                { "0", null },
                { "100", "Unknown error" },
                { "101", "Invalid parameter" },
                { "102", "The requested API does not exist" },
                { "103", "The requested method does not exist" },
                { "104", "The requested version does not support the functionality" },
                { "105", "The logged in session does not have permission" },
                { "106", "Session timeout" },
                { "107", "Session interrupted by duplicate login" },
                { "API_Auth400", "No such account or incorrect password "},
                { "API_Auth401", "Account disabled"},
                { "API_Auth402", "Permission denied"},
                { "API_Auth403", "2-step verification code required"},
                { "API_Auth404", "Failed to authenticate 2-step verification code"},
                { "DownloadStation_Task400", "File upload failed"},
                { "DownloadStation_Task401", "Max number of tasks reached"},
                { "DownloadStation_Task402", "Destination denied"},
                { "DownloadStation_Task403", "Destination does not exist"},
                { "DownloadStation_Task404", "Invalid task id"},
                { "DownloadStation_Task405", "Invalid task action"},
                { "DownloadStation_Task406", "No default destination"},
                { "DownloadStation_Task407", "Set destination failed"},
                { "DownloadStation_Task408", "File does not exist"},

            };

        private static string errorDescription(int errorCode, ExceptionType type)
        {
            string value;
            string type_ErrorCode;

            if (errorCode < 200)
                type_ErrorCode = errorCode.ToString();
            else
                type_ErrorCode = $"{type.ToString()}{errorCode}";

            errorTable.TryGetValue(type_ErrorCode, out value);
            return value;
        }

        public SynoException() : base() { }
        public SynoException(string message) : base(message) { }
        
        public static SynoException FromJson(string json, ExceptionType type)
        {
            errorObject results = JsonConvert.DeserializeObject<errorObject>(JObject.Parse(json)["error"].ToString());
            return new SynoException($"{results.code} : {errorDescription(results.code, type)}");
        }
    }

}
