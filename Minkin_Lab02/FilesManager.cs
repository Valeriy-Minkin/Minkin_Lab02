using Minkin_Lab02.Properties;
using System;
using System.Collections.Generic;
using System.IO;

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
                catch(Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CreateFolderError, path), ex);
                }
            }

            foreach (var file in newLog)
            {
                try
                {
                    File.Copy(file.Path, Path.Combine(path, file.BackupName));
                }
                catch(Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CopyFileError, file.Name), ex);
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
                catch(Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CreateFolderError, backupFolder), ex);
                }
            }
            try
            {
                File.Copy(fileData.Path, Path.Combine(backupFolder, fileData.BackupName));
            }
            catch (Exception ex)
            {
                throw new FileSystemError(string.Format(Resources.CopyFileError, fileData.Name), ex);
            }

        }

        public void ClearFolder()
        {
            foreach (var currentfolder in Cache.Instance.CurrentConfig.MonitorableFolders)
            {
                IEnumerable<string> list = Directory.EnumerateFiles(currentfolder, Constants.TxtMask, SearchOption.AllDirectories);
                foreach (var file in list)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch(Exception ex)
                    {
                        throw new FileSystemError(string.Format(Resources.DeleteFileError, file), ex);
                    }
                }
            }
        }

        public void CheckFolderExistOrCreateNewFolder(string folderName)
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName)))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName));
                }
                catch(Exception ex)
                {
                    throw new FileSystemError(string.Format(Resources.CreateFolderError, folderName),ex);
                }
            }
        }
    }
}
