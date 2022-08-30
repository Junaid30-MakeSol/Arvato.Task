using Arvato.Task.Fixer.Models.Fixer;

namespace Arvato.Task.Fixer.Interfaces
{
    public interface IFixerManager
    {
        ConversionResponseModel GetConvert(string from, string to, int amount, string date);
        RatesResponseModel GetLatestCurrency(string symbols, string bas);
    }
}
