﻿using System;
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
    /// Get a thumbnail of a file
    /// </summary>
    public class Thumb
    {
        private static string BasePath { get; } = "/webapi/***.cgi";

    }
}
