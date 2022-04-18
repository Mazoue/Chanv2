using Microsoft.AspNetCore.Components;
using Models.Chan;
using Services.Interfaces;

namespace ChanV3.Bases
{
    public class BoardsOverviewBase : ComponentBase
    {
        [Inject] private IBoardService BoardDataService { get; set; }

        public List<Board> AllBoardDetails { get; set; }

        protected override async Task OnInitializedAsync() => AllBoardDetails = await BoardDataService.FetchDetailsForAllBoards();
    }
}
