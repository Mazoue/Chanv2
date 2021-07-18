using System.Threading.Tasks;

namespace Chanv2.Interfaces
{
    public interface IDownloadService
    {
        string CleanInput(string input);
        Task<string> DownloadFileAsync(string fileUrl, string destination);
    }
}
