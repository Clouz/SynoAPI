# SynoAPI
Synology Download Station and File Station API for C#

## Status
|API NAME|DESCRIPTION|STATUS|
|---|---|:---:|
|SYNO.API.Info|Provides available API info|DONE|
|SYNO.API.Auth|Performs session login and logout|DONE|
|**Download Station API**|
|SYNO.DownloadStation.Info|Provides Download Station info and settings.Sets Download Station settings|DONE|
|SYNO.DownloadStation.Schedule|Provides advanced schedule settings. Sets advanced schedule settings|DONE|
|SYNO.DownloadStation.Task|Provides task listing and detailed task information.Performs task actions: create, delete, resume, pause|DONE|
|SYNO.DownloadStation.Statistic|Provides total download/upload statistics|DONE|
|SYNO.DownloadStation.RSS.Site|Provides RSS site listing. Refreshes RSS site||
|SYNO.DownloadStation.RSS.Feed|Provides RSS feed listing||
|SYNO.DownloadStation.BTSearch|Provides BitTorrent listing and search||
|**File Station API**||
|SYNO.FileStation.Info|Provide File Station info||
|SYNO.FileStation.List|List all shared folders, enumerate files in a shared folder,and get detailed file information||
|SYNO.FileStation.Search|Search files on given criteria||
|SYNO.FileStation.VirtualFolder|List all mount point folders of virtual file system, ex: CIFS or ISO||
|SYNO.FileStation.Favorite|Add a folder to user’s favorites or do operations on user’sfavorites||
|SYNO.FileStation.Thumb|Get a thumbnail of a file||
|SYNO.FileStation.DirSize|Get the total size of files/folders within folder(s)||
|SYNO.FileStation.MD5|Get MD5 of a file||
|SYNO.FileStation.CheckPermission|Check if the file/folder has a permission of a file/folder or not||
|SYNO.FileStation.Upload|Upload a file||
|SYNO.FileStation.Download|Download files/folders||
|SYNO.FileStation.Sharing|Generate a sharing link to share files/folders with other people and perform operations on sharing links||
|SYNO.FileStation.CreateFolder|Create folder(s)||
|SYNO.FileStation.Rename|Rename a file/folder||
|SYNO.FileStation.CopyMove|Copy/Move files/folders||
|SYNO.FileStation.Delete|Delete files/folders||
|SYNO.FileStation.Extract|Extract an archive and do operations on an archive||
|SYNO.FileStation.Compress|Compress files/folders||
|SYNO.FileStation.BackgroundTask|Get information regarding tasks of file operations which are run as the background process includingopy, move, delete, compress and extract tasks or perform operations on these background tasks||
