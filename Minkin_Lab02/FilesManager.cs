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
            FileData[] newLog = new FileData[log.files.Count];
            log.files.CopyTo(newLog);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in newLog)
            {
                try
                {
                    File.Copy(file.Path, Path.Combine(path, file.BackupName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        internal void WriteFile(FileData fileData, string backupFolder)
        {
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }
            try
            {
                File.Copy(fileData.Path, Path.Combine(backupFolder, fileData.BackupName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void ClearFolder()
        {
            foreach (var currentfolder in Cache.getInstance().CurrentConfig.MonitorableFolders)
            {
                IEnumerable<string> list = Directory.EnumerateFiles(currentfolder, "*.txt", SearchOption.AllDirectories);
                foreach (var file in list)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        Console.WriteLine("Can't delete file");
                    }
                }
            }
        }
    }
}
