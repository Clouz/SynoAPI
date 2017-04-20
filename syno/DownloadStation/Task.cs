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
        /// <param name="limit">Optional. Number of records requested: “-1” means to list all tasks. Default to “-1”</param>
        /// <param name="offset">Optional. Beginning task on the requested record. Default to “0”</param>
        /// <param name="Addittional_Detail">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_File">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Transfer">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Tracker">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Peer">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <returns></returns>
        public static ListObjects List(Init server, int limit = -1, int offset = 0, bool Addittional_Detail = true, bool Addittional_File = true, 
                                                    bool Addittional_Transfer = false, bool Addittional_Tracker = false, bool Addittional_Peer = false)
        {
            string addittional = "";
            if (Addittional_Detail)
                addittional += "detail,";
            if (Addittional_File)
                addittional += "file,";
            if (Addittional_Transfer)
                addittional += "transfer,";
            if (Addittional_Tracker)
                addittional += "tracker,";
            if (Addittional_Peer)
                addittional += "peer,";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.DownloadStation.Task&version=1&method=list&limit={limit}&offset={offset}&additional={addittional}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ListObjects results;

            try
            {
                results = JsonConvert.DeserializeObject<ListObjects>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json, SynoException.ExceptionType.DownloadStation_Task);
            }

            return results;
        }

        /// <summary>
        /// Provides task listing
        /// </summary>
        /// <param name="id">Task IDs, separated by ",".</param>
        /// <param name="Addittional_Detail">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_File">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Transfer">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Tracker">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <param name="Addittional_Peer">Optional. Additional requested info, separated by ",". When an additional option is requested, objects will be provided in the specified additional option</param>
        /// <returns></returns>
        public static ListObjects GetInfo(Init server, string id, bool Addittional_Detail = true, bool Addittional_File = true,
                                          bool Addittional_Transfer = false, bool Addittional_Tracker = false, bool Addittional_Peer = false)
        {
            string addittional = "";
            if (Addittional_Detail)
                addittional += "detail,";
            if (Addittional_File)
                addittional += "file,";
            if (Addittional_Transfer)
                addittional += "transfer,";
            if (Addittional_Tracker)
                addittional += "tracker,";
            if (Addittional_Peer)
                addittional += "peer,";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.DownloadStation.Task&version=1&method=getinfo&id={id}&additional={addittional}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            ListObjects results;

            try
            {
                results = JsonConvert.DeserializeObject<ListObjects>(JObject.Parse(json)["data"].ToString());
            }
            catch
            {
                throw syno.SynoException.FromJson(json, SynoException.ExceptionType.DownloadStation_Task);
            }

            return results;
        }

        /// <summary>
        /// Create a new download instance
        /// </summary>
        /// <param name="uri">Optional. Accepts HTTP/FTP/magnet/ED2K links or the file path starting with a shared folder, separated by ",".</param>
        /// <param name="file">Optional. File uploading from client. For more info, please see Limitations on page 30. </param>
        /// <param name="unzip_password">Optional. Password for unzipping download tasks</param>
        /// <param name="destination">Optional. Download destination path starting with a shared folder</param>
        /// <returns></returns>
        public static bool Create(Init server, string uri = "", string file = "", string unzip_password = "", string destination = "")
        {
            if (uri != "")
                uri = $"&uri={uri}";
            if (file != "")
                file = $"&file={file}";
            if (unzip_password != "")
                unzip_password = $"&unzip_password={unzip_password}";
            if (destination != "")
                destination = $"&destination={destination}";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.DownloadStation.Task&version=1&method=create{uri}{file}{unzip_password}{destination}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            Dictionary<string, string> results;

            try
            {

                results = JsonConvert.DeserializeObject<Dictionary<string, string>>(JObject.Parse(json).ToString());
            }
            catch
            {
                throw SynoException.FromJson(json, SynoException.ExceptionType.DownloadStation_Task);
            }


            return bool.Parse(results.Values.First());
        }

    }

    public class Task_DetailObjects
    {
        public int completed_time { get; set; }
        /// <summary>
        /// For BT: connected leechers For eMule: 0 
        /// </summary>
        public int connected_leechers { get; set; }
        public int connected_peers { get; set; }
        /// <summary>
        /// For BT: connected seeders For eMule: transfer source 
        /// </summary>
        public int connected_seeders { get; set; }
        /// <summary>
        /// Task created time. For more information, please see Limitations.
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// Download destination
        /// </summary>
        public string destination { get; set; }
        public int seedelapsed { get; set; }
        public int started_time { get; set; }
        /// <summary>
        /// For BT: total peers For eMule: total source 
        /// </summary>
        public int total_peers { get; set; }
        public int total_pieces { get; set; }
        public string unzip_password { get; set; }
        /// <summary>
        /// Task uri: HTTP/FTP/BT/Magnet/ED2K links
        /// </summary>
        public string uri { get; set; }
        public int waiting_seconds { get; set; }
        public string hash { get; set; }
        public int? lastSeenComplete { get; set; }
        /// <summary>
        /// Task priority. Possible values are: "auto" , " low" , " normal" , " high" .
        /// </summary>
        public string priority { get; set; }
    }

    public class Task_FileObjects
    {
        /// <summary>
        /// File name
        /// </summary>
        public string filename { get; set; }
        public int index { get; set; }
        /// <summary>
        /// Possible priority: " skip" , " low" , " high" , "normal"
        /// </summary>
        public string priority { get; set; }
        /// <summary>
        /// File size in bytes
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// Downloaded file size in bytes
        /// </summary>
        public int size_downloaded { get; set; }
        public bool wanted { get; set; }
    }

    public class Task_TrackerObjects
    {
        /// <summary>
        /// Number of peers
        /// </summary>
        public int peers { get; set; }
        /// <summary>
        /// Number of seeds
        /// </summary>
        public int seeds { get; set; }
        /// <summary>
        /// Tracker status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Next update timer
        /// </summary>
        public int update_timer { get; set; }
        /// <summary>
        /// Tracker url
        /// </summary>
        public string url { get; set; }
    }

    public class Task_TransferObjects
    {
        public int downloaded_pieces { get; set; }
        /// <summary>
        /// Task downloaded size in bytes
        /// </summary>
        public int size_downloaded { get; set; }
        /// <summary>
        /// Task uploaded size in bytes
        /// </summary>
        public int size_uploaded { get; set; }
        /// <summary>
        /// Task download speed: byte/s
        /// </summary>
        public int speed_download { get; set; }
        /// <summary>
        /// Task upload speed: byte/s
        /// </summary>
        public int speed_upload { get; set; }
    }

    public class Task_PeerObjects
    {
        /// <summary>
        /// Peer address 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Peer client name
        /// </summary>
        public string agent { get; set; }
        /// <summary>
        /// Peer progress
        /// </summary>
        public float progress { get; set; }
        /// <summary>
        /// Peer download speed: byte/s
        /// </summary>
        public int speed_download { get; set; }
        /// <summary>
        /// Peer upload speed: byte/s
        /// </summary>
        public int speed_upload { get; set; }
    }

    public class AdditionalObjects
    {
        /// <summary>
        /// A Task_Detail object
        /// </summary>
        public Task_DetailObjects detail { get; set; }
        /// <summary>
        /// Array of Task_File objects
        /// </summary>
        public List<Task_FileObjects> file { get; set; }
        /// <summary>
        /// Array of Task_Peer objects
        /// </summary>
        public List<Task_PeerObjects> peer { get; set; }
        /// <summary>
        /// Array of Task_Tracker objects
        /// </summary>
        public List<Task_TrackerObjects> tracker { get; set; }
        /// <summary>
        /// A Task_Transfer object 
        /// </summary>
        public Task_TransferObjects transfer { get; set; }
    }

    public class Status_ExtraObject
    {
        /// <summary>
        /// Available when status=extracting, ranging from 0 to 100.
        /// </summary>
        public int unzip_progress { get; set; }
        /// <summary>
        /// Available when status=error, providing error info. Possible error_detail values are listed in Appendix B: Values for Details of Erroneous Task. 
        /// </summary>
        public string error_detail{ get; set; }
    }

    public class TaskObjects
    {
        /// <summary>
        /// Optional. Additional object
        /// </summary>
        public AdditionalObjects additional { get; set; }
        /// <summary>
        /// Status_Extra object which provides extra information about task status.
        /// </summary>
        public Status_ExtraObject status_extra { get; set; }
        /// <summary>
        /// Task ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Task size in bytes
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// Current task status. Possible status values are listed in Appendix A: Download Task Status or  Appendix B: Values for Details of Erroneous Task.
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Task title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Possible types: BT, NZB, http, ftp, eMule
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Task owner
        /// </summary>
        public string username { get; set; }
    }

    public class ListObjects
    {
        public int offset { get; set; }
        public List<TaskObjects> tasks { get; set; }
        public int total { get; set; }
    }
}
