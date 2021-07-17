using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chanv2.Pages
{
    public partial class ThreadOverview
    {
        [Parameter]
        public string boardId { get; set; }

        [Parameter]
        public string threadId { get; set; }

        [Inject]
        private IThreadService ThreadService { get; set; }

        private Posts Posts { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(boardId) && !string.IsNullOrEmpty(threadId))
            {
                if (Int32.TryParse(threadId,out int threadNumber))
                {
                    Posts = await ThreadService.GetPostsInThreads(boardId, threadNumber);
                }

            }
        }

        protected int ConvertBytesToKiloBytes(int bytes)
        {
            return bytes / 1024;
        }

        protected async Task DownloadPost( Post post)
        {
            ////Base Folder
            //System.IO.Directory.CreateDirectory(DataAccessSettings.IoSettings.BaseFolder);

            ////Base Folder + BoardName
            //var baseFolderBoardName = Path.Combine(DataAccessSettings.IoSettings.BaseFolder, boardName);
            //System.IO.Directory.CreateDirectory(baseFolderBoardName);

            ////Base Folder + BoardName + ThreadName
            //threadName = CleanInput(threadName);
            //if (!string.IsNullOrEmpty(threadName))
            //{
            //    var baseFolderBoardNameThreadName = Path.Combine(baseFolderBoardName, threadName);
            //    System.IO.Directory.CreateDirectory(baseFolderBoardNameThreadName);

            //    //PostName + Extension
            //    var postName = CleanInput(post.filename);
            //    if (!string.IsNullOrEmpty(postName))
            //    {
            //        var postNameAndExtension = $"{post.filename}{post.ext}";
            //        var filePath = Path.Combine(baseFolderBoardNameThreadName, postNameAndExtension);
            //        var fileUrl = $"{boardName}/{post.tim}{post.ext}";

            //        await ImageDataService.DownloadFile(fileUrl, filePath).ConfigureAwait(false);
            //    }
            //}
        }
        //private string CleanInput(string strIn)
        //{
        //    // Replace invalid characters with empty strings.
        //    try
        //    {
        //        return Regex.Replace(strIn, @"[^\w\.@-]", "",
        //            RegexOptions.None, TimeSpan.FromSeconds(1.5));
        //    }
        //    // If we timeout when replacing invalid characters, 
        //    // we should return Empty.
        //    catch (RegexMatchTimeoutException)
        //    {
        //        return string.Empty;
        //    }
        //}
    }
}