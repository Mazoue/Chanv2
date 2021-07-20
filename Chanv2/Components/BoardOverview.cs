using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Chanv2.Pages
{
    public partial class BoardOverview
    {
        [Parameter]
        public string BoardId { get; set; }

        [Inject]
        private IBoardService BoardService { get; set; }
        [Inject]
        private IThreadService ThreadService { get; set; }

        [Inject]
        private IDownloadService DownloadService { get; set; }
        [Inject]
        private IFileSystemService FileSystemService { get; set; }


        [Inject]
        private NavigationManager NavManager { get; set; }

        private IEnumerable<Catalogue> BoarCatalogues { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (BoardId != null)
            {
                BoarCatalogues = await BoardService.GetBoardCatalog(BoardId);

            }
        }

        protected void ExpandThreadPosts(Thread currentThread)
        {
            NavManager.NavigateTo(!string.IsNullOrEmpty(currentThread.Sub)
                ? $"/thread/{BoardId}/{currentThread.No}/{HttpUtility.UrlEncode(currentThread.Sub)}"
                : $"/thread/{BoardId}/{currentThread.No}");
        }
        protected async Task DownloadThread()
        {
            foreach (var catalogue in BoarCatalogues)
            {
                var threads = catalogue.Threads;

                foreach (var thread in threads.Where(x => x.Checked).ToList())
                {
                    var threadTitle = string.IsNullOrEmpty(thread.Sub) ? thread.No.ToString() : HttpUtility.UrlDecode(thread.Sub);

                    var posts = await ThreadService.GetPostsInThreads(BoardId, thread.No);
                    foreach (var post in posts.posts.Where(x => x.fsize > 1))
                    {
                        var threadName = DownloadService.CleanInput(threadTitle);
                        var postName = DownloadService.CleanInput(post.filename);

                        var baseFolder = FileSystemService.CreateFileDestination(BoardId, threadName);
                        var filePath = FileSystemService.GenerateFilePath(baseFolder, postName, post.ext);

                        var fileUrl = $"{BoardId}/{post.tim}{post.ext}";
                        var downloadResult = await DownloadService.DownloadFileAsync(fileUrl, filePath);
                    }
                }
            }
        }

        void CheckAllClicked()
        {
            foreach (var catalogue in BoarCatalogues)
            {
                for (var index = 0; index < catalogue.Threads.Length; index++)
                {
                    catalogue.Threads[index].Checked = true;
                }
            }
            StateHasChanged();
        }


    }
}
