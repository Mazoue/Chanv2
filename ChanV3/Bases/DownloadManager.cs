using Microsoft.AspNetCore.Components;
using Models.Chan;
using Models.Downloads;
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

        public async Task DownloadFiles(DownloadRequest downloadRequest)
        {

            ShowDownloadManager = true;

            StateHasChanged();

            if(Posts == null)
            {
                Posts = new List<Post>();
            }

            foreach(var thread in downloadRequest.Threads)
            {
                foreach(var post in thread.Posts)
                {
                    Posts.Add(post);
                }
            }
            await PostService.DownloadPostsAsync(downloadRequest);
        }

        public async Task DownloadCatalogue(Catalogue? catalogue, string boardId)
        {
            if(!ShowDownloadManager)
            {
                ShowDownloadManager = true;
                StateHasChanged();
            }

            foreach(var thread in catalogue?.Threads)
            {
                var posts = await ThreadService.GetPostsInThreads(boardId, thread.No);
                await DownloadFiles(new DownloadRequest()
                {
                    Threads = new List<DownloadRequestThreadDetails>()
                    {
                        new DownloadRequestThreadDetails()
                        {
                            ThreadTitle = HttpUtility.UrlEncode(thread.Sub),
                            BoardId = boardId,
                            Posts = posts.PostCollection
                        }
                    }
                });
            }
        }
    }
}
