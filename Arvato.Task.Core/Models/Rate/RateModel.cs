using System;
namespace Arvato.Task.Core.Models.Rate
{
    public class RateModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int CurrencyId { get; set; }
        public int BaseCurrencyId { get; set; }
        public string BaseDate { get; set; }
    }
}
