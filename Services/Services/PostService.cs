using Models.Downloads;
using Services.Interfaces;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IDownloadService _downloadService;
        private readonly ILogService _logService;

        public PostService(IDownloadService downloadService, ILogService logService)
        {
            _downloadService = downloadService;
            _logService = logService;
        }
        public async Task<Stream> GetImageThumbnailAsync(string boardId, string imageId) => await _downloadService.GetImageThumbnailAsync(boardId, imageId);

        public IEnumerable<FileDownloadRequest> GenerateDownloads(DownloadRequest downloadRequest)
        {
            var fileDownloads = new List<FileDownloadRequest>();

            //foreach(var post in downloadRequest.Thread.Posts)
            //{
            //    if(post.images > 0)
            //    {
            //        var baseFolder = BuildFolderStructure(downloadRequest.Thread.ThreadTitle, downloadRequest.Thread.BoardId);

            //        var filePath = GenerateFilePath(baseFolder, post.filename, post.ext);

            //        var fileUrl = $"{downloadRequest.Thread.BoardId}/{post.tim}{post.ext}";

            //        fileDownloads.Add(new FileDownloadRequest()
            //        {
            //            FilePath = filePath,
            //            FileUrl = fileUrl
            //        });
            //    }
            //}

            Parallel.ForEach(downloadRequest.Thread.Posts, post =>
            {
                if(!string.IsNullOrEmpty(post.filename) && !string.IsNullOrEmpty(post.ext))
                {
                    var baseFolder = BuildFolderStructure(downloadRequest.Thread.ThreadTitle, downloadRequest.Thread.BoardId).Trim();

                    var filePath = GenerateFilePath(baseFolder, post.filename, post.ext);

                    var fileUrl = $"{downloadRequest.Thread.BoardId}/{post.tim}{post.ext}";

                    fileDownloads.Add(new FileDownloadRequest()
                    {
                        FilePath = filePath,
                        FileUrl = fileUrl
                    });
                }
            });

            return fileDownloads;
        }

        public async Task DownloadPostsAsync(IEnumerable<FileDownloadRequest> downloadRequests)
        {
            try
            {
                //THIS SHOULD BE LIMITED TO 1 CALL PER SECOND PER THE TOS
                var rateLimit = new SemaphoreSlim(1);
                Parallel.ForEach(downloadRequests, async post =>
                 {
                     if(post != null && !string.IsNullOrEmpty(post.FileUrl) && !string.IsNullOrEmpty(post.FilePath))
                     {
                         await rateLimit.WaitAsync();
                         try
                         {

                             await _downloadService.DownloadFileAsync(post.FileUrl, post.FilePath);
                             await Task.Delay(1000);

                         }
                         catch(Exception ex)
                         {
                             await _logService.WriteError(ex);
                         }
                         rateLimit.Release();
                     }
                 });
            }
            catch(Exception ex)
            {
                await _logService.WriteError(ex);
            }
        }

        private string BuildFolderStructure(string? threadTitle, string boardId)
        {
            var threadName = !string.IsNullOrEmpty(threadTitle) ? _downloadService.CleanInput(threadTitle) : "No Title";
            var baseFolder = _downloadService.CreateFileDestination(boardId, threadName);
            return baseFolder;
        }

        private string GenerateFilePath(string baseFolder, string fileName, string fileExtension)
        {
            var postName = _downloadService.CleanInput(fileName);
            var filePath = _downloadService.GenerateFilePath(baseFolder, postName, fileExtension);
            return filePath;
        }
    }
}
