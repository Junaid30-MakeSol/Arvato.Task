using Arvato.Task.DB.Models.Currency.Dto;

namespace Arvato.Task.DB.Interfaces
{
    public interface ICurrencyRepository
    {
        int Create(CurrencyDto model);
    }
}
