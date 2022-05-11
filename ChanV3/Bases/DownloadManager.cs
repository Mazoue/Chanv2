using Microsoft.AspNetCore.Components;
using Models.Chan;
using Services.Interfaces;

namespace ChanV3.Pages
{
    public partial class DownloadManager
    {
        [Inject]
        private IPostService PostService { get; set; }

        private List<Post> Posts { get; set; }

        private bool ShowDownloadManager { get; set; }

        public async Task DownloadFiles(string threadTitle, IEnumerable<Post> posts, string boardId)
        {

            ShowDownloadManager = true;

            StateHasChanged();

            Posts = posts.ToList();

            await PostService.DownloadPostsAsync(threadTitle, posts, boardId);

        }
    }
}
