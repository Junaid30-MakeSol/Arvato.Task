using Arvato.Task.DB.Models.Base.Dto;
namespace Arvato.Task.DB.Interfaces
{
    public interface IBaseRepository
    {
        int Create(BaseDto model);
    }
}
