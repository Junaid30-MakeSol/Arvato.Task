using Arvato.Task.DB.Models.Rate.Dto;

namespace Arvato.Task.DB.Interfaces
{
    public interface IRateRepository
    {
        void Create(RateDto model);
    }
}
