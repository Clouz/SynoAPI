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

namespace syno.DownloadStation
{
    /// <summary>
    /// Provides total download/upload statistics.
    /// </summary>
    public class Statistic
    {
        private static string BasePath { get; } = "/webapi/DownloadStation/statistic.cgi";

        /// <summary>
        /// Provides total download/upload statistics
        /// </summary>
        /// <param name="id">Task IDs to be set destination, separated by ","</param>
        /// <param name="destination">Optional. Download destination path starting with a shared folder</param>
        /// <returns></returns>
        public static Statistic_TotalInfo GetInfo(Init server)
        {
            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.DownloadStation.Statistic&version=1&method=getinfo",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            Statistic_TotalInfo results;

            try { results = JsonConvert.DeserializeObject<Statistic_TotalInfo>(JObject.Parse(json)["data"].ToString()); }
            catch { throw SynoException.FromJson(json, SynoException.ExceptionType.DownloadStation_Task); }

            return results;
        }

        public class Statistic_TotalInfo
        {
            /// <summary>
            /// Total download speed except for eMule: byte/s
            /// </summary>
            public int speed_download { get; set; }
            /// <summary>
            /// Total upload speed except for eMule: byte/s
            /// </summary>
            public int speed_upload { get; set; }
            /// <summary>
            /// Total eMule download speed: byte/s
            /// </summary>
            public int emule_speed_download { get; set; }
            /// <summary>
            /// Total eMule upload speed: byte/s
            /// </summary>
            public int emule_speed_upload { get; set; }
        }
    }
}
