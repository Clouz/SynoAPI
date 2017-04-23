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
    /// Get MD5 of a file
    /// </summary>
    class MD5
    {
        private static string BasePath { get; } = "/webapi/***.cgi";

    }
}
