using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;

namespace syno.API
{

    /// <summary>
    /// Performs session login and logout
    /// </summary>
    public class Auth
    {
        private static string BasePath { get; } = "/webapi/auth.cgi";

        /// <summary>
        /// Performs session login
        /// </summary>
        /// <param name="session">Login session name</param>
        /// <param name="format">Returned format of session ID. Following are the two possible options and the default value is cookie. cookie: The login session ID will be set to cookie. sid: The login sid will only be returned as response json data and the cookie will not be set.</param>
        /// <param name="otp_code">This option is not required to log into Download Station sessions currently. However, please note that DSM 4.2 and later includes a 2-step verification option. If enabled, the user requires a verification code to log into DSM sessions.</param>
        /// <returns>Authorized session ID. When the user log in with format=sid, cookie will not be set and each API request should provide a request parameter sid=<sid> along with other parameters.</returns>
        public static SessionObject GetLogin(Init server, string session = "DownloadStation", string format = "cookie", string otp_code = null)
        {
            string APIList = $"api=SYNO.API.Auth&version=2&method=login&account={server.Username}&passwd={server.Password}&session={session}&format={format}";
            if (otp_code != null)
                APIList += $"&otp_code={otp_code}";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = APIList,
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;

            Dictionary<string, string> results;
            SessionObject stato = new SessionObject();

            try
            {
                results = JsonConvert.DeserializeObject<Dictionary<string, string>>(JObject.Parse(json)["data"].ToString());
                stato.sid = results.Values.First();
            }
            catch
            {
                throw SynoException.FromJson(json);
            }

            return stato;
        }

        /// <summary>
        /// Performs session logout
        /// </summary>
        /// <param name="session">Logout session name</param>
        /// <returns></returns>
        public static bool GetLogout(Init server, string session = "DownloadStation")
        {
            string APIList = $"api=SYNO.API.Auth&version=1&method=logout&session={session}";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = APIList,
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;

            Dictionary<string, string> results;

            results = JsonConvert.DeserializeObject<Dictionary<string, string>>(JObject.Parse(json).ToString());
            
            return bool.Parse(results.Values.First());
        }

        public class SessionObject
        {
            /// <summary>
            /// Authorized session ID. When the user log in with format=sid, cookie will not be set and each API request should provide a request parameter sid=<sid> along with other parameters.
            /// </summary>
            public string sid { get; set; }
        }

    }
}
