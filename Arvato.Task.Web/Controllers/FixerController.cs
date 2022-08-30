using Arvato.Task.Fixer.Interfaces;
using Arvato.Task.Fixer.Models.Fixer;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Arvato.Task.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixerController : ControllerBase
    {
        private readonly IFixerManager _fixerManager;

        public FixerController(IFixerManager fixerManager)
        {
            _fixerManager = fixerManager;
        }
        // GET: api/<FixerController

        [HttpGet]
        [Route("currency/conversion")]
        public IActionResult GetCurrencyConvert(QueryModel model)
        {
            var data =  _fixerManager.GetConvert(model.To, model.From, model.Amount, model.Date);
            return Ok(data);
        }


        //[HttpGet]
        //[Route("currency/latest")]
        //public IActionResult GetLatestCurency()
        //{
        //    RecurringJob.AddOrUpdate("Run on daily basis",() => _fixerManager.GetLatestCurrency("", ""), Cron.Daily);

        //    return Ok($"Recurring Job Scheduled. Invoice will be mailed Monthly!");
        //}


    }
}
