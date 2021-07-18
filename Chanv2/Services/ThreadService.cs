using Chanv2.DataModels;
using Chanv2.Interfaces;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chanv2.Services
{
    public class ThreadService : IThreadService
    {
        private readonly HttpClient _httpClient;

        public ThreadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Posts> GetPostsInThreads(string boardId, int threadId)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<Posts>
                    (await _httpClient.GetStreamAsync($"{boardId}/thread/{threadId}.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
