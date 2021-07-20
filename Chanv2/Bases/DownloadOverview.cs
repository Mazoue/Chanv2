using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chanv2.Pages
{
    public partial class DownloadOverview : ComponentBase
    {
        //[Parameter]
        //public Posts Posts { get; set; }
        //[Parameter]
        //public string ThreadTitle { get; set; }
        //[Parameter]
        //public string BoardName { get; set; }

        [Inject]
        private IDownloadService DownloadService { get; set; }
        [Inject]
        private IFileSystemService FileSystemService { get; set; }


        public async Task DownloadPost(string threadTitle,Posts posts, string boardId)
        {
            
                foreach (var post in posts.posts)
                {
                    var threadName = DownloadService.CleanInput(threadTitle);
                    var postName = DownloadService.CleanInput(post.filename);

                    var baseFolder = FileSystemService.CreateFileDestination(boardId, threadName);
                    var filePath = FileSystemService.GenerateFilePath(baseFolder, postName, post.ext);

                    var fileUrl = $"{boardId}/{post.tim}{post.ext}";
                    var downloadResult = await DownloadService.DownloadFileAsync(fileUrl, filePath);
                }
            
        }

    }
}
