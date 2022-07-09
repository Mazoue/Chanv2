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
            CheckState();

            if(Posts == null)
            {
                Posts = new List<Post>();
            }

            foreach(var post in downloadRequest.Thread.Posts)
            {
                Posts.Add(post);
            }

            var downloads = PostService.GenerateDownloads(downloadRequest);

            if(downloads != null && downloads.Any())
            {
                await PostService.DownloadPostsAsync(downloads);
            }
        }

        public async Task DownloadCatalogue(Catalogue? catalogue, string boardId)
        {
            if(catalogue != null)
            {
                foreach(var thread in catalogue.Threads)
                {
                    var posts = await ThreadService.GetPostsInThreads(boardId, thread.No);
                    await DownloadFiles(new DownloadRequest()
                    {
                        Thread = new DownloadRequestThreadDetails()
                        {
                            ThreadTitle = HttpUtility.UrlEncode(thread.Sub),
                            BoardId = boardId,
                            Posts = posts.PostCollection
                        }
                    });
                }
            }
        }

        public async Task DownloadBoard(BoardDownloadRequest boardDownloadRequest)
        {
            foreach(var catalogue in boardDownloadRequest.Catalogues)
            {
                await DownloadCatalogue(catalogue, boardDownloadRequest.BoardId);
            }
        }

        private void CheckState()
        {
            if(!ShowDownloadManager)
            {
                ShowDownloadManager = true;
                StateHasChanged();
            }
        }
    }
}
