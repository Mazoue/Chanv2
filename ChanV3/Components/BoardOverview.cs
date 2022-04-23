using Microsoft.AspNetCore.Components;
using Models.Chan;
using Services.Interfaces;
using System.Web;

namespace ChanV3.Pages
{
    public partial class BoardOverview
    {
        [Parameter]
        public string BoardId { get; set; }

        [Parameter]
        public string BoardName { get; set; }

        [Inject]
        private NavigationManager NavManager { get; set; }
        [Inject]
        private IBoardService BoardService { get; set; }


        private string BoardTitle { get; set; }
        private string ThreadTitle { get; set; }


        private IEnumerable<Catalogue> Catalogues { get; set; }

        protected override void OnInitialized()
        {
        }

        protected override async Task OnParametersSetAsync()
        {
            if(BoardId != null)
            {
                Catalogues = await BoardService.GetBoardCatalogue(BoardId);
            }

            BoardTitle = !string.IsNullOrEmpty(BoardName) ? BoardName : BoardId;
        }

        protected void ExpandThreadPosts(ChanThread currentThread) => NavManager.NavigateTo(!string.IsNullOrEmpty(currentThread.Sub)
                ? $"/thread/{BoardId}/{currentThread.No}/{HttpUtility.UrlEncode(currentThread.Sub)}"
                : $"/thread/{BoardId}/{currentThread.No}");


        private void CheckAllThreads(object isChecked)
        {
            foreach (var catalog in Catalogues)
            {
                foreach (var currentThread in catalog.Threads)
                {
                    currentThread.Checked = (bool)isChecked;
                }
            }
            StateHasChanged();
        }



    }
}
