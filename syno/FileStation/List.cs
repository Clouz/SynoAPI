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
    /// List all shared folders, enumerate files in a shared folder, and get detailed file information
    /// </summary>
    public class List
    {
        private static string BasePath { get; } = "/webapi/entry.cgi";

        /// <summary>
        /// List all shared folders
        /// </summary>
        /// <param name="offset">Optional. Specify how many shared folders are skipped before beginning to return listed shared folders</param>
        /// <param name="limit">Optional. Number of shared folders requested. 0 lists all shared folders</param>
        /// <param name="sort">Optional. Specify which file information to sort on.  Options include: [name: file name ; user: file owner ; group: file group ; mtime: last modified time ; atime: last access time ; ctime: last change time ; crtime: create time  ; posix: POSIX permission]</param>
        /// <param name="direction">Optional. Specify to sort ascending or to sort descending. Options include: [ asc: sort ascending ; desc: sort descending ]</param>
        /// <param name="onlywritable">Optional. “true”: List writable shared folders; “false”: List writable and read-only shared folders.</param>
        /// <param name="additional">Optional. Additional requested file information separated by a comma “, “ and around the brackets. When an additional option is requested, responded objects will be provided in the specified additional option. Options include: [ real_path: return a real path in volume ; size: return file byte size ; owner: return information ; about file owner including user name, group name, UID and GID ; time: return information about time including last access time, last modified time, last change time and create time ; perm: return information about file permission  ; mount_point_type: return a type of a virtual file system of a mount point ; volume_status: return volume  ; statuses including free space, total space and read-only status ]</param>
        /// <returns></returns>
        public static list_shareObject list_share(Init server, int? offset = 0, int? limit = 0, sort_by? sort = sort_by.name, sort_direction? direction = sort_direction.asc, bool? onlywritable = false, string additional = null)
        {
            string _offset = "";
            string _limit = "";
            string _sort = "";
            string _direction = "";
            string _onlywritable = "";
            string _additional = "";
            

            if (offset != null) _offset = $"&offset={offset}";
            if (limit != null) _limit = $"&limit={limit}";
            if (sort != null) _sort = $"&sort={sort}";
            if (direction != null) _direction = $"&direction={direction}";
            if (onlywritable != null) _onlywritable = $"&onlywritable={onlywritable}";
            if (additional != null) _additional = $"&additional={additional}";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.FileStation.List&version=2&method=list_share{_offset}{_limit}{_sort}{_direction}{_onlywritable}{_additional}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            list_shareObject results;

            try { results = JsonConvert.DeserializeObject<list_shareObject>(JObject.Parse(json)["data"].ToString()); }
            catch { throw syno.SynoException.FromJson(json, SynoException.ExceptionType.API_Info); }

            return results;
        }

        /// <summary>
        /// Enumerate files in a given folder
        /// </summary>
        /// <param name="folder_path">A listed folder path started with a shared folder</param>
        /// <param name="offset">Optional. Specify how many files are skipped before beginning to return listed files.</param>
        /// <param name="limit">Optional. Number of files requested. 0 indicates to list all files with a given folder.</param>
        /// <param name="sort">Optional. Specify which file information to sort on.</param>
        /// <param name="direction">Optional. Specify to sort ascending or to sort descending</param>
        /// <param name="pattern">Optional. Given glob pattern(s) to find files whose names and extensions match a case-insensitive glob pattern.</param>
        /// <param name="filetype">Optional. “file”: only enumerate regular files; “dir”: only enumerate folders; “all” enumerate regular files and folders</param>
        /// <param name="goto_path">Optional. Folder path started with a shared folder. Return all files and sub-folders within folder_path path until goto_path path recursively. </param>
        /// <param name="additional">Optional. Additional requested file information separated by a comma “, “and around the brackets. When an additional option is requested, responded objects will be provided in the specified additional option. Options include: [real_path: return a real path in volume ; size: return file byte size ; owner: return information about file owner including user name, group name, UID and GID ; time: return information about time including last access time, last modified time, last change time and create time ; perm: return information about file permission]</param>
        /// <returns></returns>
        public static list_shareObject list(Init server, string folder_path, int? offset = 0, int? limit = 0, sort_by? sort = sort_by.name, sort_direction? direction = sort_direction.asc, string pattern = null, string filetype = null, string goto_path = null, string additional = null)
        {
            // TODO: da finire
            string _offset = "";
            string _limit = "";
            string _sort = "";
            string _direction = "";
            string _pattern = "";
            string _filetype = "";
            string _goto_path = "";
            string _additional = "";


            if (offset != null) _offset = $"&offset={offset}";
            if (limit != null) _limit = $"&limit={limit}";
            if (sort != null) _sort = $"&sort={sort}";
            if (direction != null) _direction = $"&direction={direction}";
            if (pattern != null) _pattern = $"&pattern={pattern}";
            if (goto_path != null) _goto_path = $"&goto_path={goto_path}";
            if (filetype != null) _filetype = $"&filetype={filetype}";
            if (additional != null) _additional = $"&additional={additional}";

            Uri fullPath = new UriBuilder(server.BaseAddress)
            {
                Path = BasePath,
                Query = $"api=SYNO.FileStation.List&version=2&method=list_share{_offset}{_limit}{_sort}{_direction}{_pattern}{_goto_path}{_filetype}{_additional}",
            }.Uri;

            Console.WriteLine(fullPath);

            string json = Init.Richiesta(fullPath).Result;
            list_shareObject results;

            try { results = JsonConvert.DeserializeObject<list_shareObject>(JObject.Parse(json)["data"].ToString()); }
            catch { throw syno.SynoException.FromJson(json, SynoException.ExceptionType.API_Info); }

            return results;
        }

        public enum sort_by
        {
            name,
            user,
            group,
            mtime,
            atime,
            ctime,
            crtime,
            posix
        }

        public enum sort_direction
        {
            asc,
            desc
        }

        public class adv_rightObject
        {
            /// <summary>
            /// If a non-administrator user can download files in this shared folder through SYNO.FileStation.Download API or not
            /// </summary>
            public bool disable_download { get; set; }
            /// <summary>
            /// If a non-administrator user can enumerate files in this shared folder though SYNO.FileStation.List API with list method or not. 
            /// </summary>
            public bool disable_list { get; set; }
            /// <summary>
            /// If a non-administrator user can modify or overwrite files in this shared folder or not.
            /// </summary>
            public bool disable_modify { get; set; }
        }

        public class aclObject
        {
            /// <summary>
            /// If a logged-in user has a privilege to append data or create folders within this folder or not. 
            /// </summary>
            public bool append { get; set; }
            /// <summary>
            /// If a logged-in user has a privilege to delete a file/a folder within this folder or not
            /// </summary>
            public bool del { get; set; }
            /// <summary>
            /// If a logged-in user has a privilege to execute files/traverse folders within this folder or not.
            /// </summary>
            public bool exec { get; set; }
            /// <summary>
            /// If a logged-in user has a privilege to read data or list folder within this folder or not
            /// </summary>
            public bool read { get; set; }
            /// <summary>
            /// If a logged-in user has a privilege to write data or create files within this folder or not
            /// </summary>
            public bool write { get; set; }
        }

        public class sharedFolderPermObject
        {
            /// <summary>
            /// “RW: The shared folder is writable; “RO”: the shared folder is read-only
            /// </summary>
            public string share_right { get; set; }
            /// <summary>
            /// POSIX file permission, For example, 777 means owner, group or other has all permission; 764 means owner has all permission, group has read/write permission, other has read permission
            /// </summary>
            public int posix { get; set; }
            /// <summary>
            /// Specail privelge of the shared folder
            /// </summary>
            public adv_rightObject adv_right { get; set; }
            /// <summary>
            /// If the configure of Windows ACL privilege of the shared folder is enabled or not
            /// </summary>
            public bool acl_enable { get; set; }
            /// <summary>
            /// “true”: The privilege of the shared folder is set to be ACL-mode. “false”: The privilege of the shared folder is set to be POSIX-mode.
            /// </summary>
            public bool is_acl_mode { get; set; }
            /// <summary>
            /// Windows ACL privilege. If a shared folder is set to be POSIX-mode, these values of Windows ACL privileges are derived from the POSIX privilege.
            /// </summary>
            public aclObject acl { get; set; }
        }

        public class timeObject
        {
            /// <summary>
            /// Linux timestamp of last access in second
            /// </summary>
            public string atime { get; set; }
            /// <summary>
            /// Linux timestamp of last modification in second
            /// </summary>
            public string mtime { get; set; }
            /// <summary>
            /// Linux timestamp of last change in second
            /// </summary>
            public string ctime { get; set; }
            /// <summary>
            /// Linux timestamp of create time in second
            /// </summary>
            public string crtime { get; set; }
            /// <summary>
            /// Windows ACL privilege. If a shared folder is set to be POSIX-mode, these values of Windows ACL privileges are derived from the POSIX privilege
            /// </summary>
            public aclObject acl { get; set; }
        }

        public class ownerObject
        {
            /// <summary>
            /// User name of file owner
            /// </summary>
            public string user { get; set; }
            /// <summary>
            /// Group name of file group
            /// </summary>
            public string group { get; set; }
            /// <summary>
            /// File UID
            /// </summary>
            public int uid { get; set; }
            /// <summary>
            /// File GID
            /// </summary>
            public int gid { get; set; }
        }

        public class sharedFolderAdditionalObject
        {
            /// <summary>
            /// Real path of a shared folder in a volume space
            /// </summary>
            public string real_path { get; set; }
            /// <summary>
            /// File owner information including user name, group name, UID and GID
            /// </summary>
            public ownerObject owner { get; set; }
            public timeObject time { get; set; }

        }

        public class sharedFolderObject
        {
            /// <summary>
            /// 
            /// </summary>
            public bool isdir { get; set; }
            /// <summary>
            /// Name of a shared folder
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// Path of a shared folder
            /// </summary>
            public string path { get; set; }
            /// <summary>
            /// Shared-folder additional object
            /// </summary>
            public sharedFolderAdditionalObject additional { get; set; }
        }

        public class list_shareObject
        {
            /// <summary>
            /// Requested offset
            /// </summary>
            public int offset { get; set; }
            /// <summary>
            /// Array of <shared folder> objects
            /// </summary>
            public List<sharedFolderObject> shares { get; set; }
            /// <summary>
            /// Total number of shared folders
            /// </summary>
            public int total { get; set; }
        }
    }
}
