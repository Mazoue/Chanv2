using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Chanv2.Services;

namespace Chanv2.Pages
{
    public partial class BoardOverview
    {
        [Parameter]
        public string BoardId { get; set; }

        [Parameter]
        public string BoardName { get; set; }

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

        private string BoardTitle { get; set; }
        private string ThreadTitle { get; set; }

        private DownloadOverview DownloadOverview { get; set; }
        private IEnumerable<Catalogue> BoarCatalogs { get; set; }

        protected override void OnInitialized()
        {
            DownloadOverview = new DownloadOverview();
        }
        protected override async Task OnParametersSetAsync()
        {
            if (BoardId != null)
            {
                BoarCatalogs = await BoardService.GetBoardCatalog(BoardId);
            }

            BoardTitle = !string.IsNullOrEmpty(BoardName) ? BoardName : BoardId;
        }

        protected void ExpandThreadPosts(Thread currentThread)
        {
            NavManager.NavigateTo(!string.IsNullOrEmpty(currentThread.Sub)
                ? $"/thread/{BoardId}/{currentThread.No}/{HttpUtility.UrlEncode(currentThread.Sub)}"
                : $"/thread/{BoardId}/{currentThread.No}");
        }
        protected async Task DownloadThreads()
        {
            foreach (var catalog in BoarCatalogs)
            {
                var threads = catalog.Threads;

                foreach (var thread in from thread in threads.Where(x => x.Checked).ToList() let threadTitle = string.IsNullOrEmpty(thread.Sub)
                    ? thread.No.ToString()
                    : HttpUtility.UrlDecode(thread.Sub) select thread)
                {
                    var posts = await ThreadService.GetPostsInThreads(BoardId, thread.No);
                    await DownloadOverview.DownloadPost(string.IsNullOrEmpty(thread.Sub) ? thread.No.ToString() : HttpUtility.UrlDecode(thread.Sub), posts.posts.Where(x => x.fsize > 1), BoardId).ConfigureAwait(false);
                }
            }
        }

        private void CheckAllClicked()
        {
            foreach (var catalog in BoarCatalogs)
            {
                foreach (var currentThread in catalog.Threads)
                {
                    currentThread.Checked = true;
                }
            }
            StateHasChanged();
        }
    }
}
