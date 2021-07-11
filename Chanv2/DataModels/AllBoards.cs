using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chanv2.DataModels
{
    public class AllBoards
    {
        [JsonProperty("boards")]
        public List<Board> Boards { get; set; }
    }
}
