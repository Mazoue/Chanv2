using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Chanv2.Pages
{
    public partial class BoardOverview
    {
        [Parameter]
        public string boardId { get; set; }

        [Inject]
        private IBoardService BoardService { get; set; }

        private IEnumerable<Catalogue> BoarCatalogues { get; set; }


        protected override async Task OnInitializedAsync()
        {
            BoarCatalogues = await BoardService.GetBoardCatalog(boardId);
        }

        private void BoardCatalogue_Changed()
        {

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
