using Arvato.Task.Fixer.Models.Fixer;

namespace Arvato.Task.Fixer.Interfaces
{
    public interface IFixerCLient
    {
        RatesResponseModel GetLatestRates();
        RatesResponseModel GetRatesByDate(string date);
    }
}
