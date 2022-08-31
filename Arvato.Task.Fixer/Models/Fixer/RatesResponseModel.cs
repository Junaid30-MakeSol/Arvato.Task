using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arvato.Task.Fixer.Models.Fixer
{
    public class RatesResponseModel
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }

    }

}
