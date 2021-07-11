using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chanv2.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Chanv2.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private IBoardService BoardService { get; set; }
        
        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

    }
}
