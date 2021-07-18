using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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
        private NavigationManager NavManager { get; set; }

        private IEnumerable<Catalogue> BoarCatalogues { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (BoardId != null)
            {
                BoarCatalogues = await BoardService.GetBoardCatalog(BoardId);

            }
        }

        protected async Task DownloadThreadPosts(Thread currentThread)
        {
        }

        protected void ExpandThreadPosts(Thread currentThread)
        {
            NavManager.NavigateTo(!string.IsNullOrEmpty(currentThread.Sub)
                ? $"/thread/{BoardId}/{currentThread.No}/{HttpUtility.UrlEncode(currentThread.Sub)}"
                : $"/thread/{BoardId}/{currentThread.No}");
        }


    }
}
