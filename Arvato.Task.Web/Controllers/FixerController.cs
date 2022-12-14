using Arvato.Task.Core.Interfaces;
using Arvato.Task.Fixer.Models.Fixer;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Arvato.Task.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixerController : ControllerBase
    {
        private readonly IRateManager _rateManager;

        public FixerController(IRateManager rateManager)
        {
            _rateManager = rateManager;
        }

       [HttpGet]
       [Route("currency/conversion")]
        public IActionResult GetCurrencyConvert(QueryModel model)
        {
            var data = _rateManager.GetConvertCurrencyRates(model.To, model.From, model.Amount, model.Date);
            return Ok(data);
        }


        [HttpGet]
        [Route("currency/latest")]
        public IActionResult GetLatestCurency()
        {
            RecurringJob.AddOrUpdate("Run on daily basis", () => _rateManager.GetLatestCurrencyRates(), Cron.Daily);

            return Ok($"Recurring Job Scheduled. Invoice will be mailed Monthly!");
        }

    }
}
