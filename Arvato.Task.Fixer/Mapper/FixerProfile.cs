using Arvato.Task.DB.Models.Base.Dto;
using Arvato.Task.DB.Models.Currency.Dto;
using Arvato.Task.DB.Models.Rate.Dto;
using Arvato.Task.Fixer.Models.Base;
using Arvato.Task.Fixer.Models.Currency;
using Arvato.Task.Fixer.Models.Rate;
using AutoMapper;

namespace Arvato.Task.Fixer.Mapper
{
    public class FixerProfile:Profile
    {
        public FixerProfile()
        {
            CreateMap<BaseModel,BaseDto>().ReverseMap();
            CreateMap<CurrencyModel,CurrencyDto>().ReverseMap();
            CreateMap<RateModel, RateDto>().ReverseMap();
        }
    }
}
