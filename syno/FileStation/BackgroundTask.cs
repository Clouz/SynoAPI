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
    /// Get information regarding tasks of file operations which are run as the background process including copy, move, delete, compress and extract tasks or perform operations on these background tasks
    /// </summary>
    class BackgroundTask
    {
        private static string BasePath { get; } = "/webapi/***.cgi";

    }
}
