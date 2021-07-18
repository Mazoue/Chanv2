using Chanv2.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chanv2.Interfaces
{
    public interface IBoardService
    {
        Task<AllBoards> GetAllBoardsDetails();
        Task<IEnumerable<Catalogue>> GetBoardCatalog(string board);
    }
}
