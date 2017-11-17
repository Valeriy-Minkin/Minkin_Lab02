using Minkin_Lab02.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minkin_Lab02
{
    public class FilesManager
    {
        public void WriteFileList(CurrentFilesCondition log, string path)
        {
            FileData[] newLog = new FileData[log.Files.Count];
            log.Files.CopyTo(newLog);
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    throw new Exception(Resources.CreateFolderError);
                }
            }

            foreach (var file in newLog)
            {
                try
                {
                    File.Copy(file.Path, Path.Combine(path, file.BackupName));
                }
                catch
                {
                    throw new Exception(Resources.CopyFileError);
                }
            }
        }

        internal void WriteFile(FileData fileData, string backupFolder)
        {
            if (!Directory.Exists(backupFolder))
            {
                try
                {
                    Directory.CreateDirectory(backupFolder);
                }
                catch
                {
                    throw new Exception(Resources.CreateFolderError);
                }
            }
            try
            {
                File.Copy(fileData.Path, Path.Combine(backupFolder, fileData.BackupName));
            }
            catch (Exception ex)
            {
                throw new Exception(Resources.CopyFileError);
            }

        }

        public void ClearFolder()
        {
            foreach (var currentfolder in Cache.getInstance().CurrentConfig.MonitorableFolders)
            {
                IEnumerable<string> list = Directory.EnumerateFiles(currentfolder, Resources.TxtMask, SearchOption.AllDirectories);
                foreach (var file in list)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        throw new Exception(Resources.DeleteFileError);
                    }
                }
            }
        }
    }
}
