using Arvato.Task.Fixer.Models.Fixer;

namespace Arvato.Task.Core.Interfaces
{
    public interface IRateManager
    {
        ConversionResponseModel GetConvertCurrencyRates(string from, string to, int amount, string date);
        RatesResponseModel GetLatestCurrencyRates();
    }
}

