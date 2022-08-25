using Arvato.Task.Fixer.Models.Fixer;

namespace Arvato.Task.Fixer.Interfaces
{
    public interface IFixerManager
    {
        ConversionResponseModel GetConvert(string to, string from, int amount, string date);
        void GetLatestCurrency(string symbols, string bas);
    }
}
