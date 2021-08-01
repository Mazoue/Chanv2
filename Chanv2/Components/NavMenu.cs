using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Chanv2.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private IBoardService BoardService { get; set; }
        
        private bool _collapseNavMenu = true;

        private string NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}
