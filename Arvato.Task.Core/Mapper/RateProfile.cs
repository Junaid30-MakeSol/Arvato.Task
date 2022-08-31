using Arvato.Task.Core.Models.Currency;
using Arvato.Task.Core.Models.Rate;
using Arvato.Task.DB.Models.Currency.Dto;
using Arvato.Task.DB.Models.Rate.Dto;
using AutoMapper;

namespace Arvato.Task.Core
{
    public class RateProfile:Profile
    {
        public RateProfile()
        {
           
            CreateMap<CurrencyModel, CurrencyDto>();
            CreateMap<CurrencyDto, CurrencyModel>();
            CreateMap<RateModel, RateDto>();
            CreateMap<RateDto, RateModel>();
        }
    }
}
