using Models.Downloads;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<Stream> GetImageThumbnailAsync(string boardId, string imageId);

        IEnumerable<FileDownloadRequest> GenerateDownloads(DownloadRequest downloadRequest);

        Task DownloadPostsAsync(IEnumerable<FileDownloadRequest> downloadRequest);
    }
}
