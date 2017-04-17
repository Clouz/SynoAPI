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
    /// Provides task listing and detailed task information.
    /// Performs task actions: create, delete, resume, pause.
    /// </summary>
    public class Tasks
    {
        private static string BasePath { get; } = "/webapi/DownloadStation/task.cgi";

        /// <summary>
        /// Provides task listing
        /// </summary>
        /// <returns></returns>
        public static ListObject List(Init server)
        {
            // TODO: Aggiungere tutti i possibili additional e rifare le classi di conseguenza con http://json2csharp.com/
            // TODO: detail, transfer, file, tracker, peer

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.DownloadStation.Task&version=1&method=list&additional=detail,file",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ListObject results;

            try
            {
                results = JsonConvert.DeserializeObject<ListObject>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json);
            }

            return results;
        }
    }

    public class DetailObject
    {
        public int completed_time { get; set; }
        public int connected_leechers { get; set; }
        public int connected_peers { get; set; }
        public int connected_seeders { get; set; }
        public int create_time { get; set; }
        public string destination { get; set; }
        public int seedelapsed { get; set; }
        public int started_time { get; set; }
        public int total_peers { get; set; }
        public int total_pieces { get; set; }
        public string unzip_password { get; set; }
        public string uri { get; set; }
        public int waiting_seconds { get; set; }
        public string hash { get; set; }
        public int? lastSeenComplete { get; set; }
        public string priority { get; set; }
    }

    public class FileObject
    {
        public string filename { get; set; }
        public int index { get; set; }
        public string priority { get; set; }
        public int size { get; set; }
        public int size_downloaded { get; set; }
        public bool wanted { get; set; }
    }

    public class AdditionalObject
    {
        public DetailObject detail { get; set; }
        public List<FileObject> file { get; set; }
    }

    public class TaskObject
    {
        public AdditionalObject additional { get; set; }
        public string id { get; set; }
        public int size { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string username { get; set; }
    }

    public class ListObject
    {
        public int offset { get; set; }
        public List<TaskObject> tasks { get; set; }
        public int total { get; set; }
    }
}
