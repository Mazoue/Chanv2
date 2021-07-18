using Chanv2.DataModels;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Chanv2.Bases
{
    public class BoardsOverviewBase : ComponentBase
    {
        [Inject] private IBoardService BoardDataService { get; set; }

        public AllBoards AllBoardDetails { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AllBoardDetails = await BoardDataService.GetAllBoardsDetails();

        }
    }
}
