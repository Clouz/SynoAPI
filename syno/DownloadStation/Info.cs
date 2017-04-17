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
    /// Provides Download Station info and settings.
    /// Sets Download Station settings.
    /// </summary>
    public class Info
    {
        private static string BasePath { get; } = "/webapi/DownloadStation/info.cgi";

        /// <summary>
        /// Provides Download Station info
        /// </summary>
        /// <returns></returns>
        public static InfoObject GetInfo(Init server)
        {

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Info&version=1&method=getinfo",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            InfoObject results;

            try
            {
                results = JsonConvert.DeserializeObject<InfoObject>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json);
            }

            return results;
        }

        public static ConfigObject GetConfig(Init server)
        {

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Info&version=1&method=getconfig",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ConfigObject results;

            try
            {
                results = JsonConvert.DeserializeObject<ConfigObject>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json);
            }

            return results;
        }

        // todo: Trovare un modo per passare singolarmente tutti i parametri
        public static void SetConfig(Init server, ConfigObject conf)
        { 
            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Info&version=1&method=getconfig",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ConfigObject results;

            try
            {
                results = JsonConvert.DeserializeObject<ConfigObject>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json);
            }
        }
    }

    public class InfoObject
    {
        /// <summary>
        /// If the logged in user is manager 
        /// </summary>
        public string is_manager { get; set; }
        /// <summary>
        /// Build number of Download Station 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// Full version string of Download Station 
        /// </summary>
        public string version_string { get; set; }
    }

    public class ConfigObject
    {
        /// <summary>
        /// Max BT download speed in KB/s (“0” means unlimited) 
        /// </summary>
        public int bt_max_download { get; set; }
        /// <summary>
        /// Max BT upload speed in KB/s (“0” means unlimited)
        /// </summary>
        public int bt_max_upload { get; set; }
        /// <summary>
        /// Max eMule download speed in KB/s (“0” means unlimited)
        /// </summary>
        public int emule_max_download { get; set; }
        /// <summary>
        /// Max eMule upload speed in KB/s (“0” means unlimited)
        /// </summary>
        public int emule_max_upload { get; set; }
        /// <summary>
        /// Max NZB download speed in KB/s (“0” means unlimited)
        /// </summary>
        public int nzb_max_download { get; set; }
        /// <summary>
        /// Max HTTP download speed in KB/s (“0” means unlimited). For more info, please see Limitations
        /// </summary>
        public int http_max_download { get; set; }
        /// <summary>
        /// Max FTP download speed in KB/s (“0” means unlimited). For more info, please see Limitations.
        /// </summary>
        public int ftp_max_download { get; set; }
        /// <summary>
        /// If eMule service is enabled
        /// </summary>
        public bool emule_enabled { get; set; }
        /// <summary>
        ///  If Auto unzip service is enabled for users except admin or administrators group
        /// </summary>
        public bool unzip_service_enabled { get; set; }
        /// <summary>
        ///  Default destination
        /// </summary>
        public string default_destination { get; set; }
        /// <summary>
        /// Emule default destination
        /// </summary>
        public string emule_default_destination { get; set; }

        public string prova { get; set; }
    }
}
