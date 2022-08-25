using Newtonsoft.Json;
using System;

namespace Arvato.Task.Fixer.Models.Fixer
{
    public class Info
    {
        [JsonProperty("capturedAmount")]
        public double CapturedAmount { get; set; }
        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }
    }

    public class Query
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
    }

    public class ConversionResponseModel
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("historical")]
        public string Historical { get; set; }
        [JsonProperty("info")]
        public Info Info { get; set; }
        [JsonProperty("query")]
        public Query Query { get; set; }
        [JsonProperty("result")]
        public double Result { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    public class QueryModel
    {
        public int Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Date { get; set; }
    }

}
