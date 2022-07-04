using Models.Downloads;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<Stream> GetImageThumbnailAsync(string boardId, string imageId);
        Task DownloadPostsAsync(DownloadRequest downloadRequest);
    }
}
