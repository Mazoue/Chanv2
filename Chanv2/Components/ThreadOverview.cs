using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
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

        [Inject]
        private IDownloadService DownloadService { get; set; }
        [Inject]
        private IFileSystemService FileSystemService { get; set; }

        private Posts Posts { get; set; }

        private string DisplayTitle { get; set; }

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

        protected async Task DownloadPost(Post post)
        {
            var threadName = DownloadService.CleanInput(ThreadTitle);
            var postName = DownloadService.CleanInput(post.filename);

            var baseFolder = FileSystemService.CreateFileDestination(BoardId, threadName);
            var filePath = FileSystemService.GenerateFilePath(baseFolder, postName, post.ext);

            var fileUrl = $"{BoardId}/{post.tim}{post.ext}";
            var downloadResult = await DownloadService.DownloadFileAsync(fileUrl, filePath);

        }

    }
}