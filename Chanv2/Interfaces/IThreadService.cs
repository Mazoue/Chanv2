using Chanv2.DataModels;
using System.Threading.Tasks;

namespace Chanv2.Interfaces
{
    public interface IThreadService
    {
        Task<Posts> GetPostsInThreads(string boardId, int threadId);
    }
}
