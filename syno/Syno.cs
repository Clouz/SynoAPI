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
    }


    [Serializable()]
    public class SynoException : Exception
    {
        public class errorObject
        {
            public int code { get; set; }
        }

        private static Dictionary<int, string> errorTable = new Dictionary<int, string>()
            {
                { 0, null },
                { 100, "Unknown error" },
                { 101, "Invalid parameter" },
                { 102, "The requested API does not exist" },
                { 103, "The requested method does not exist" },
                { 104, "The requested version does not support the functionality" },
                { 105, "The logged in session does not have permission" },
                { 106, "Session timeout" },
                { 107, "Session interrupted by duplicate login" },
                { 400, "No such account or incorrect password "},
                { 401, "Account disabled"},
                { 402, "Permission denied"},
                { 403, "2-step verification code required"},
                { 404, "Failed to authenticate 2-step verification code"},
            };

        private static string errorDescription(int errorCode)
        {
            string value;
            errorTable.TryGetValue(errorCode, out value);
            return value;
        }


        public SynoException() : base() { }
        public SynoException(string message) : base(message) { }
        
        public static SynoException FromJson(string json)
        {
            errorObject results = JsonConvert.DeserializeObject<errorObject>(JObject.Parse(json)["error"].ToString());
            return new SynoException($"{results.code} : {errorDescription(results.code)}");
        }

    }
}
