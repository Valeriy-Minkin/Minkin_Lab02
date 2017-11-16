﻿using System;
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
        public void WriteFileList(Log log, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in log.files)
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
    }
}