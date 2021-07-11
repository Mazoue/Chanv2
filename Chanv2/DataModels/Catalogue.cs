using Newtonsoft.Json;

namespace Chanv2.DataModels
{
    public class Catalogue
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("Threads")]
        public Thread[] Threads { get; set; }
    }
}
