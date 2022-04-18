using Models.Chan;

namespace Services.Interfaces
{
    public interface IPostService
    {
        Task<Stream> GetImageThumbnailAsync(string boardId, string imageId);
        Task DownloadPostsAsync(string threadTitle, IEnumerable<Post> posts, string boardId);
    }
}
