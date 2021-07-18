using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Chanv2.Interfaces;

namespace Chanv2.Services
{
    public class FileSystemService : IFileSystemService
    {
        private string BaseFolder { get; set; } 

        public FileSystemService(string baseFolder)
        {
            BaseFolder = baseFolder;
        }

        public string CreateFileDestination(string boardName, string threadName)
        {
            //Base Folder
            System.IO.Directory.CreateDirectory(BaseFolder);

            //Base Folder + BoardName
            var baseFolderBoardName = Path.Combine(BaseFolder, boardName);
            System.IO.Directory.CreateDirectory(baseFolderBoardName);

            //Base Folder + BoardName + ThreadName
            var baseFolderBoardNameThreadName = Path.Combine(baseFolderBoardName, threadName);
            System.IO.Directory.CreateDirectory(baseFolderBoardNameThreadName);

            return baseFolderBoardNameThreadName;

        }
        public string GenerateFilePath(string baseFolder, string fileName, string fileExtension)
        {
            var postNameAndExtension = $"{fileName}{fileExtension}";
            var filePath = Path.Combine(baseFolder, postNameAndExtension);
            return filePath;
        }
    }
}
