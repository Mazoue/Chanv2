using Microsoft.AspNetCore.Components;
using Models.Chan;
using Services.Interfaces;
using System.Web;

namespace ChanV3.Pages
{
    public partial class DownloadManager
    {
        [Inject]
        private IPostService PostService { get; set; }

        [Inject]
        private IThreadService ThreadService { get; set; }

        private List<Post> Posts { get; set; }

        private bool ShowDownloadManager { get; set; }

        public async Task DownloadFiles(string threadTitle, IEnumerable<Post> posts, string boardId)
        {

            ShowDownloadManager = true;

            StateHasChanged();

            Posts = posts.ToList();

            await PostService.DownloadPostsAsync(threadTitle, posts, boardId);

        }

        public async Task DownloadThread(ChanThread? thread, string boardId)
        {

            ShowDownloadManager = true;

            StateHasChanged();

            if (int.TryParse(thread.No, out var threadNumber))
            {
                var posts = await ThreadService.GetPostsInThreads(boardId, threadNumber);
                await DownloadFiles(HttpUtility.UrlEncode(thread.Sub), posts, boardId);
            }
        }
    }
}
