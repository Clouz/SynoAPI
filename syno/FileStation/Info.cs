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

namespace syno.FileStation
{
    /// <summary>
    /// Provide File Station info
    /// </summary>
    public class Info
    {
        private static string BasePath { get; } = "/webapi/entry.cgi";

        /// <summary>
        /// Provide File Station info
        /// </summary>
        public static DataObject GetInfo(Init server)
        {

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = "api=SYNO.FileStation.Info&version=2&method=get",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            DataObject results;

            try { results = JsonConvert.DeserializeObject<DataObject>(JObject.Parse(json)["data"].ToString()); }
            catch { throw syno.SynoException.FromJson(json, SynoException.ExceptionType.API_Info); }

            return results;
        }


        public class DataObject
        {
            /// <summary>
            /// If the logged-in user is an administrator
            /// </summary>
            public bool is_manager { get; set; }
            /// <summary>
            /// Types of virtual file systems which the logged user is able to mount. DSM 6.0 supports CIFS, NFS, and ISO virtual file systems. Different types are separated with a comma, for example: cifs,nfs,iso
            /// </summary>
            public List<string> support_virtual_protocol { get; set; }
            /// <summary>
            /// Whether the logged-in user can share file(s)/folder(s) or not
            /// </summary>
            public bool support_sharing { get; set; }
            /// <summary>
            /// DSM hostname
            /// </summary>
            public string hostname { get; set; }
        }
    }
}
