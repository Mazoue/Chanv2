using Chanv2.DataModels;
using Chanv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chanv2.Services
{
    public class BoardService : IBoardService
    {
        private readonly HttpClient _httpClient;

        public BoardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AllBoards> GetAllBoardsDetails()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<AllBoards>
                    (await _httpClient.GetStreamAsync($"boards.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Catalogue>> GetBoardCatalog(string board)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Catalogue>>(await _httpClient.GetStreamAsync($"/{board}/catalog.json"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
