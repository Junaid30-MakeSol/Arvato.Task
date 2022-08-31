using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Models.Fixer;
using log4net;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Net;
using System.Reflection;

namespace Arvato.Task.Fixer.Managers
{
    public class FixerClient : IFixerCLient
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IConfiguration _configuration;
        public FixerClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public RatesResponseModel GetLatestRates()
        {
            _log.Debug($"Started Get Latest Rate");
            try
            {
                var authServerHost = _configuration["Fixer:Host"];

                var tokenEndpoint = string.Format(@"{0}fixer/latest", authServerHost);

                var client = new RestClient(tokenEndpoint);

                var fixerApiKey = _configuration["Fixer:APIKey"];
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("apikey", fixerApiKey);

                var result = client.Execute<RatesResponseModel>(request);
                if (result != null && result.StatusCode == HttpStatusCode.OK) return result.Data;

                _log.Info(string.Format("Response of authorize request: {0}", result.Content));
                return null;
            }
            catch (Exception ex)
            {

                _log.Error($"Error: Get Latest currency", ex);
                return null;
            }
        }


        public RatesResponseModel GetRatesByDate(string date)
        {
            _log.Debug($"Started Get Rate by Date:{date}");
            try
            {
                var authServerHost = _configuration["Fixer:Host"];
                var tokenEndpoint = string.Format(@"{0}fixer/{1}", authServerHost, date);
                var client = new RestClient(tokenEndpoint);

                var fixerApiKey = _configuration["Fixer:APIKey"];
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("apikey", fixerApiKey);

                var result = client.Execute<RatesResponseModel>(request);
                if (result != null && result.StatusCode == HttpStatusCode.OK) return result.Data;
                _log.Info(string.Format("Response of authorize request: {0}", result.Content));
                return result.Data;
            }
            catch(Exception ex)
            {

                _log.Error($"Error: Get Rates by Date:{date}", ex);
                return null;
            }
        }


    }
}
