using Chanv2.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chanv2.Interfaces
{
    public interface IBoardService
    {
        Task<AllBoards> GetAllBoardsDetails();
        Task<IEnumerable<Catalogue>> GetBoardCatalog(string board);
    }
}
