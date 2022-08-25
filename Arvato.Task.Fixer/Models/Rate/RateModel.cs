namespace Arvato.Task.Fixer.Models.Rate
{
    public class RateModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int CurrencyId { get; set; }
        public int BaseId { get; set; }
    }
}
