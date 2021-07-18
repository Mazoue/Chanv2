using Chanv2.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        public async Task<string> DownloadFileAsync(string fileUrl, string destination)
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

        public string CleanInput(string input)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(input, @"[^\w\.@-]", "",
                    RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return string.Empty;
            }
        }
    }
}
