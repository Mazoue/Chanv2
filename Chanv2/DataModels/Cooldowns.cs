using Newtonsoft.Json;

namespace Chanv2.DataModels
{
    public class Cooldowns
    {
        [JsonProperty("threads")]
        public int Threads { get; set; }

        [JsonProperty("replies")]
        public int Replies { get; set; }

        [JsonProperty("images")]
        public int Images { get; set; }
    }
}
