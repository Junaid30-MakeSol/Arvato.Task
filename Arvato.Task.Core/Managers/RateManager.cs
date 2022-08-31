using Arvato.Task.Core.Interfaces;
using Arvato.Task.Core.Models.Currency;
using Arvato.Task.Core.Models.Rate;
using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Currency.Dto;
using Arvato.Task.DB.Models.Rate.Dto;
using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Models.Fixer;
using AutoMapper;
using System;

namespace Arvato.Task.Core.Managers
{
    public class RateManager : IRateManager
    {
        private readonly IFixerCLient _fixerClient;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        public RateManager(IFixerCLient fixerCLient, ICurrencyRepository currencyRepository, IRateRepository rateRepository, IMapper mapper)
        {
            _fixerClient = fixerCLient;
            _currencyRepository = currencyRepository;
            _rateRepository = rateRepository;
            _mapper = mapper;
        }
        public ConversionResponseModel GetConvertCurrencyRates(string from, string to, int amount, string date)
        {
            if (!string.IsNullOrEmpty(date) && from != null && to != null && amount > 0)
            {
               var latestRatesByDate = _fixerClient.GetRatesByDate(date);
               var rate =  CurrencyConversion(latestRatesByDate, from, to, amount);

                var data = new ConversionResponseModel
                {
                    Date = date,
                    Query = new Query
                    {
                         Amount = amount,
                         From = from,
                         To = to,
                    },
                    Result = rate,
                };

                Console.WriteLine($"from {from} to {to}: {rate}", from, to, rate);
                return data;
            }

            else if(string.IsNullOrEmpty(date) && from != null && to != null && amount > 0)
            {
                var latestRates = _fixerClient.GetLatestRates();
                var rate = CurrencyConversion(latestRates, from, to, amount);
                var data = new ConversionResponseModel
                {
                    Date = date,
                    Query = new Query
                    {
                        Amount = amount,
                        From = from,
                        To = to,
                    },
                    Result = rate,

                };

                Console.WriteLine($"from {from} to {to}: {rate}", from, to, rate);
                return data;
            }
            return null;
        }

        public RatesResponseModel GetLatestCurrencyRates()
        {
           var result = _fixerClient.GetLatestRates();
           if (result != null)
           {
             SaveDataIntoDb(result);
           }
           return result;
            
        }

        void SaveDataIntoDb(RatesResponseModel model)
        {
            _currencyRepository.Delete();
            _rateRepository.Delete();

            var baseCurrencyModel = new CurrencyDto
            {
                Name = model.Base,
            };

            var BasecurrencyId = _currencyRepository.Create(baseCurrencyModel);

            foreach (var keyValue in model.Rates)
            {
                var currencyModel = new CurrencyModel
                {
                    Name = keyValue.Key,
                };

                var currencyMapper = _mapper.Map<CurrencyDto>(currencyModel);
                var currencyId = _currencyRepository.Create(currencyMapper);

                var rateModel = new RateModel
                {

                    CurrencyId = currencyId,
                    Value = keyValue.Value,
                    BaseDate = model.Date,
                    BaseCurrencyId = BasecurrencyId

                };
                var rateMapper = _mapper.Map<RateDto>(rateModel);
                _rateRepository.Create(rateMapper);

            }
        }

        double CurrencyConversion(RatesResponseModel model, string from, string to, int amount)
        {
            model.Rates.TryGetValue(from, out var returnedFromValue);
            model.Rates.TryGetValue(to, out var returnedToValue);
            double fromValue = returnedFromValue;
            double toValue = returnedToValue;
            var conversionRate = toValue / fromValue;
            var result = conversionRate * amount;
            return result;
        }
    }
}
