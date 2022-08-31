using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Models.Fixer;
using log4net;
using Microsoft.Extensions.Configuration;
using RestSharp;
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
            var requestUrl = string.Format(_configuration["Fixer:LatestRates"],"","");
            var client = new RestClient(requestUrl);

            var fixerApiKey = _configuration["Fixer:APIKey"];
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("apikey", fixerApiKey);

            var result = client.Execute<RatesResponseModel>(request);
            if (result != null && result.StatusCode == HttpStatusCode.OK) return result.Data;

            _log.Info(string.Format("Response of authorize request: {0}", result.Content));
            return null;
        }


        public RatesResponseModel GetRatesByDate(string date)
        {
            _log.Debug($"Started Get Rate by Date:{date}");
            var requestUrl = string.Format(_configuration["Fixer:RatesByDate"],date,"","");
            var client = new RestClient(requestUrl);

            var fixerApiKey = _configuration["Fixer:APIKey"];
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("apikey", fixerApiKey);

            var result = client.Execute<RatesResponseModel>(request);
            if (result != null && result.StatusCode == HttpStatusCode.OK) return result.Data;
            _log.Info(string.Format("Response of authorize request: {0}", result.Content));
            return result.Data;
        }

        //public RatesResponseModel GetLatestRates(string symbols, string bas)
        //{
        //    _log.Debug($"Started GetLatest Currenecy request with symbols:{symbols} - base: {bas}");
        //    var requestUrl = string.Format(_configuration["Fixer:LatestRates"], symbols, bas);
        //    var client = new RestClient(requestUrl);

        //    var fixerApiKey = _configuration["Fixer:APIKey"];
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("content-type", "application/json");
        //    request.AddHeader("apikey", fixerApiKey);

        //    var result = client.Execute<RatesResponseModel>(request);
        //    if (result != null && result.StatusCode == HttpStatusCode.OK) return result.Data;
        //    _log.Info(string.Format("Response of authorize request: {0}", result.Content));
        //    return result.Data;
        //}


    }
}
