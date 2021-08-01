using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Chanv2.Pages
{
    public partial class ThreadOverview
    {
        [Parameter]
        public string BoardId { get; set; }

        [Parameter]
        public string ThreadId { get; set; }
        [Parameter]
        public string ThreadTitle { get; set; }

        [Inject]
        private IThreadService ThreadService { get; set; }

        private Posts Posts { get; set; }


        private string DisplayTitle { get; set; }

        private DownloadOverview DownloadOverview { get; set; }

        

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(BoardId) && !string.IsNullOrEmpty(ThreadId))
            {
                if (int.TryParse(ThreadId, out var threadNumber))
                {
                    Posts = await ThreadService.GetPostsInThreads(BoardId, threadNumber);
                }
            }
            DisplayTitle = string.IsNullOrEmpty(ThreadTitle) ? ThreadId : HttpUtility.UrlDecode(ThreadTitle);
        }

        protected int ConvertBytesToKiloBytes(int bytes)
        {
            return bytes / 1024;
        }

        void CheckAllClicked()
        {
            for (var index = 0; index < Posts.posts.ToList().Count; index++)
            {
                if (Posts.posts[index].fsize > 1)
                {
                    Posts.posts[index].Checked = true;
                }
            }
            StateHasChanged();
        }

    }
}