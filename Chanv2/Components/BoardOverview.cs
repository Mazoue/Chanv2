using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chanv2.Pages
{
    public partial class BoardOverview
    {
        [Parameter]
        public string boardId { get; set; }

        [Inject]
        private IBoardService BoardService { get; set; }

        private IEnumerable<Catalogue> BoarCatalogues { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (boardId != null)
            {
                BoarCatalogues = await BoardService.GetBoardCatalog(boardId);
                
            }
        }
             

        protected async Task DownloadThreadPosts(Thread currentThread)
        {
            //FullBoard.CurrentThreadId = currentThread.no;
            //FullBoard.CurrentThreadName = !string.IsNullOrEmpty(currentThread.sub) ? currentThread.sub : "Misc";
            //await GetThreadPosts(FullBoard.CurrentBoard, currentThread.no).ConfigureAwait(false);
            //await ParseBoardPosts(FullBoard.Posts, FullBoard.CurrentBoard, FullBoard.CurrentThreadName)
            //    .ConfigureAwait(false);

        }

        protected async Task ExpandThreadPosts(Thread currentThread)
        {
            //    FullBoard.CurrentThreadId = currentThread.no;
            //    FullBoard.CurrentThreadName = !string.IsNullOrEmpty(currentThread.sub) ? currentThread.sub : "Misc";
            //    await GetThreadPosts(FullBoard.CurrentBoard, currentThread.no).ConfigureAwait(false);
            //    FullBoard.CurrentStage = LoadingStage.Posts;
        }


    }
}
