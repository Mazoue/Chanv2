using Chanv2.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chanv2.Services
{
    public class DownloadService : IDownloadService
    {
        private readonly HttpClient _httpClient;

        public DownloadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DownloadFile(string fileUrl, string destination)
        {
            try
            {
                var directoryName = Path.GetDirectoryName(destination);
                if (!string.IsNullOrEmpty(directoryName))
                {
                    if (!Directory.Exists(directoryName))
                    {
                        System.IO.Directory.CreateDirectory(directoryName);
                    }
                }

                using var result = await _httpClient.GetAsync(fileUrl).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    await File.WriteAllBytesAsync(destination,
                            await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return "done";
        }
    }
}
