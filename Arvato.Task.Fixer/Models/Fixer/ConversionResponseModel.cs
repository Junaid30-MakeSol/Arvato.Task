using Newtonsoft.Json;
using System;

namespace Arvato.Task.Fixer.Models.Fixer
{

    public class Query
    {
        public int Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    public class ConversionResponseModel
    {
        public string Date { get; set; }
        public Query Query { get; set; }
        public double Result { get; set; }

    }

    public class QueryModel
    {
        public int Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Date { get; set; }
    }

}
