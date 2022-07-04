using Models.Downloads;
using Services.Interfaces;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IDownloadService _downloadService;

        public PostService(IDownloadService downloadService) => _downloadService = downloadService;
        public async Task<Stream> GetImageThumbnailAsync(string boardId, string imageId) => await _downloadService.GetImageThumbnailAsync(boardId, imageId);

        public async Task DownloadPostsAsync(DownloadRequest downloadRequest)
        {
            try
            {
                //THIS SHOULD BE LIMITED TO 1 CALL PER SECOND PER THE TOS
                var rateLimit = new SemaphoreSlim(1);
                Parallel.ForEach(downloadRequest.Threads, async thread =>
                {
                    Parallel.ForEach(thread.Posts, async post =>
                    {
                        if(post.images > 0)
                        {
                            var threadName = !string.IsNullOrEmpty(thread.ThreadTitle) ? _downloadService.CleanInput(thread.ThreadTitle) : "No Title";
                            var postName = _downloadService.CleanInput(post.filename);

                            var baseFolder = _downloadService.CreateFileDestination(thread.BoardId, threadName);
                            var filePath = _downloadService.GenerateFilePath(baseFolder, postName, post.ext);

                            var fileUrl = $"{thread.BoardId}/{post.tim}{post.ext}";

                            await rateLimit.WaitAsync();
                            await _downloadService.DownloadFileAsync(fileUrl, filePath);
                            await Task.Delay(1000);
                            rateLimit.Release();
                        }
                    });
                });
            }
            catch(Exception ex)
            {
                var t = ex;
            }
        }
    }
}
