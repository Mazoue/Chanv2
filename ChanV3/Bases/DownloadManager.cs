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

        public async Task DownloadCatalogue(Catalogue? catalogue, string boardId)
        {

            ShowDownloadManager = true;

            StateHasChanged();

            foreach(var thread in catalogue?.Threads)
            {
                var posts = await ThreadService.GetPostsInThreads(boardId, thread.No);
                await DownloadFiles(HttpUtility.UrlEncode(thread.Sub), posts.PostCollection, boardId);
            }
        }
    }
}
