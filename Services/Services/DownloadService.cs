using DataAccess.Interfaces;
using Services.Interfaces;
using System.Text.RegularExpressions;

namespace Services.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IFileSystemRepository _fileSystemRepository;

        public DownloadService(IContentRepository contentRepository, IFileSystemRepository fileSystemRepository)
        {
            _contentRepository = contentRepository;
            _fileSystemRepository = fileSystemRepository;
        }
        public async Task<Stream> GetImageThumbnailAsync(string boardId, string imageId) => await _contentRepository.GetImageThumbnail(boardId, imageId);

        public async Task DownloadFileAsync(string fileUrl, string destination)
        {
            var imageResponse = await _contentRepository.GetImage(fileUrl);
            await _fileSystemRepository.WriteImageToDestination(destination, imageResponse);
        }

        public string CreateFileDestination(string boardId, string threadName)
        {
            return _fileSystemRepository.CreateFileDestination(boardId, threadName);
        }

        public string GenerateFilePath(string baseFolder, string fileName, string fileExtension)
        {
            return _fileSystemRepository.GenerateFilePath(baseFolder,fileName,fileExtension);
        }

        public string CleanInput(string input)
        {
            // Replace invalid characters with empty strings.
            try
            {
                input = input.Replace(".", "");
                return Regex.Replace(input, @"[^\w\.@-]", "",
                    RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return string.Empty;
            }
        }        
    }
}
