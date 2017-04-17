using System;
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
    /// Provides available API info
    /// </summary>
    public class Info
    {
        private static string BasePath { get; } = "/webapi/query.cgi";

        /// <summary>
        /// Provides available API info
        /// </summary>
        /// <param name="query">API names concatenated by "," or use "ALL" to get all supported APIs</param>
        /// <returns></returns>
        public static Dictionary<string, ApiObject> GetInfo(Init server, string query = "ALL")
        {
            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.API.Info&version=1&method=query&query={query}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
 
            Dictionary<string, ApiObject> results = JsonConvert.DeserializeObject<Dictionary<string, ApiObject>>(JObject.Parse(json)["data"].ToString());

            return results;
        }
    }

    /// <summary>
    /// API name 
    /// </summary>
    public class ApiObject
    {
        /// <summary>
        /// API cgi path 
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// Minimum API version supported 
        /// </summary>
        public string minVersion { get; set; }
        /// <summary>
        /// Maximum API version supported 
        /// </summary>
        public string maxVersion { get; set; }
    }
}
