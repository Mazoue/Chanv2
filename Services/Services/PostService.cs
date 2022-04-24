using Models.Chan;
using Services.Interfaces;

namespace Services.Services
{
    public class PostService : IPostService
    {
        private readonly IDownloadService _downloadService;

        public PostService(IDownloadService downloadService) 
        { 
            _downloadService = downloadService;
        }
        public async Task<Stream> GetImageThumbnailAsync(string boardId, string imageId) => await _downloadService.GetImageThumbnailAsync(boardId, imageId);

        public async Task DownloadPostsAsync(string threadTitle, IEnumerable<Post> posts, string boardId)
        {
            try
            {

            //THIS SHOULD BE LIMITED TO 1 CALL PER SECOND PER THE TOS
            var rateLimit = new SemaphoreSlim(1);

            Parallel.ForEach(posts, async post =>
            {
                var threadName = _downloadService.CleanInput(threadTitle);
                var postName = _downloadService.CleanInput(post.filename);

                var baseFolder = _downloadService.CreateFileDestination(boardId, threadName);
                var filePath = _downloadService.GenerateFilePath(baseFolder, postName, post.ext);

                var fileUrl = $"{boardId}/{post.tim}{post.ext}";


                await rateLimit.WaitAsync();
                await _downloadService.DownloadFileAsync(fileUrl, filePath);
                await Task.Delay(1000);
                rateLimit.Release();
            });
            }
            catch (Exception ex)
            {

                var t = ex;
            }


        }
    }
}
