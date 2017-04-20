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
    /// Provides advanced schedule settings.
    /// Sets advanced schedule settings.
    /// </summary>
    public class Schedule
    {
        private static string BasePath { get; } = "/webapi/DownloadStation/schedule.cgi";

        /// <summary>
        /// Provides advanced schedule settings 
        /// </summary>
        /// <returns></returns>
        public static ScheduleConfigObject GetConfig(Init server)
        {

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Schedule&version=1&method=getconfig",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ScheduleConfigObject results;

            try
            {
                results = JsonConvert.DeserializeObject<ScheduleConfigObject>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json, SynoException.ExceptionType.DownloadStation_Schedule);
            }

            return results;
        }

        /// <summary>
        /// set advanced schedule settings
        /// </summary>
        /// <param name="conf">It represents the parameters to be set, if null will not be considered</param>
        /// <returns></returns>
        public static bool SetConfig(Init server, ScheduleConfigObject conf)
        {
            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Schedule&version=1&method=setconfig" + syno.Init.GetParameters<ScheduleConfigObject>(conf),
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;

            var results = JsonConvert.DeserializeObject<Dictionary<string, string>>(JObject.Parse(json).ToString());

            return bool.Parse(results.Values.First());
        }
    }

    public class ScheduleConfigObject
    {
        /// <summary>
        /// If download schedule is enabled
        /// </summary>
        public bool? enabled { get; set; }
        /// <summary>
        /// If eMule download schedule is enabled
        /// </summary>
        public bool? emule_enabled { get; set; }
    }
}
