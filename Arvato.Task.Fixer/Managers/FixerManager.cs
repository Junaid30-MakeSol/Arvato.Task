using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Base.Dto;
using Arvato.Task.DB.Models.Currency.Dto;
using Arvato.Task.DB.Models.Rate.Dto;
using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Models.Base;
using Arvato.Task.Fixer.Models.Currency;
using Arvato.Task.Fixer.Models.Fixer;
using Arvato.Task.Fixer.Models.Rate;
using AutoMapper;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Arvato.Task.Fixer.Managers
{
    public class FixerManager : IFixerManager
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConfiguration _configuration;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        public FixerManager(IMapper mapper, IConfiguration configuration, ICurrencyRepository currencyRepository, IBaseRepository baseRepository, IRateRepository rateRepository)
        {
            _configuration = configuration;
            _baseRepository = baseRepository;
            _currencyRepository = currencyRepository;
            _rateRepository = rateRepository;
            _mapper = mapper;
        }
        public ConversionResponseModel GetConvert(string to, string from, int amount, string date)
        {
            _log.Debug($"Started GetConvert Currenecy request with to:{to} - from: {from} - amount: {amount}");
            var requestUrl = string.Format(_configuration["Fixer:CurrencyConvert"], to, from, amount, date);
            var client = new RestClient(requestUrl);

            var fixerApiKey = _configuration["Fixer:APIKey"];
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("apikey", fixerApiKey);

            var response = client.Execute<ConversionResponseModel>(request);
            _log.Info(string.Format("Response of authorize request: {0}", response.Content));
            Console.WriteLine(response.Content);
            return response.Data;
        }

        public RatesResponseModel GetLatestCurrency(string symbols, string bas)
        {
            _log.Debug($"Started GetLatest Currenecy request with symbols:{symbols} - base: {bas}");
            var requestUrl = string.Format(_configuration["Fixer:LatestCurrency"], symbols, bas);
            var client = new RestClient(requestUrl);

            var fixerApiKey = _configuration["Fixer:APIKey"];
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("apikey", fixerApiKey);

            var response = client.Execute<RatesResponseModel>(request);
            _log.Info(string.Format("Response of authorize request: {0}", response.Content));

            var dictionary = new List<Dictionary<string, double>>();


            dictionary.Add(new Dictionary<string, double>()
            {
                { "AED", response.Data.Rates.AED },
                { "AFN", response.Data.Rates.AFN },
                { "JPY", response.Data.Rates.JPY },
            }

            );


            SaveDataIntoDb(response.Data, dictionary);

            Console.WriteLine(response.Content);
            return response.Data;
        }

        void SaveDataIntoDb(RatesResponseModel model, List<Dictionary<string, double>> keyValues)
        {
            var baseModel = new BaseModel
            {
                Date = Convert.ToDateTime(model.Date),
                Name = model.Base
            };

            var baseMapper = _mapper.Map<BaseDto>(baseModel);
            var baseId = _baseRepository.Create(baseMapper);


            foreach (var keyValue in keyValues)
            {
                foreach (var item in keyValue)
                {

                    var currencyModel = new CurrencyModel
                    {
                        Name = item.Key,
                    };

                    var currencyMapper = _mapper.Map<CurrencyDto>(currencyModel);
                    var currencyId = _currencyRepository.Create(currencyMapper);




                    var rateModel = new RateModel
                    {
                        BaseId = baseId,
                        CurrencyId = currencyId,
                        Value = item.Value
                    };

                    var rateMapper = _mapper.Map<RateDto>(rateModel);
                    _rateRepository.Create(rateMapper);


                }

            }
        }
    }
}
