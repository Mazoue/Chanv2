using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chanv2.Bases
{
    public class DownloadOverview : ComponentBase
    {
        [Inject]
        private IDownloadService DownloadService { get; set; }
        [Inject]
        private IFileSystemService FileSystemService { get; set; }

        public int TotalFileCount { get; set; } = 0;
        public int ProcessedFileCount { get; set; } = 0;

        public async Task DownloadPost(string threadTitle, IEnumerable<Post> posts, string boardId)
        {
            TotalFileCount += posts.Count();

            //THIS SHOULD BE LIMITED TO 1 CALL PER SECOND PER THE TOS
            var rateLimit = new SemaphoreSlim(1);

            Parallel.ForEach( posts, async post => 
            {
                var threadName = DownloadService.CleanInput(threadTitle);
                var postName = DownloadService.CleanInput(post.filename);

                var baseFolder = FileSystemService.CreateFileDestination(boardId, threadName);
                var filePath = FileSystemService.GenerateFilePath(baseFolder, postName, post.ext);

                var fileUrl = $"{boardId}/{post.tim}{post.ext}";
                
                
                await rateLimit.WaitAsync();                
                var downloadResult = await DownloadService.DownloadFileAsync(fileUrl, filePath);
                await Task.Delay(1000);
                rateLimit.Release();

                ProcessedFileCount += 1;

                await InvokeAsync(StateHasChanged);
            });

        }

    }
}