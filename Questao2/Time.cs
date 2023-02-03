using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2
{
    public class Time
    {
        [JsonProperty("competition")]
        public string Competition { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }

        [JsonProperty("team1")]
        public string Team1 { get; set; }

        [JsonProperty("team2")]
        public string Team2 { get; set; }

        [JsonProperty("team1goals")]
        public int Team1goals { get; set; }

        [JsonProperty("team2goals")]
        public int Team2goals { get; set; }
    }
}
