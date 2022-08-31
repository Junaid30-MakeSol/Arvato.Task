using Arvato.Task.Console;
using Arvato.Task.Core.Interfaces;
using Arvato.Task.Fixer.Interfaces;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Arvato.Task
{
    public class Program
    {

        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            
            // Startup.cs finally :)
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IApplicationBuilder appBuilder = new ApplicationBuilder(serviceProvider);
            RecurringJobManager recurringJobManager = new RecurringJobManager();
            startup.Configure(appBuilder);

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Logger is working!");


            var rateManager = serviceProvider.GetService<IRateManager>();


            // Get Service and call method

            System.Console.WriteLine("Select what you want to do");
            System.Console.WriteLine("Select 1 for Converting Currency rate");
            System.Console.WriteLine("Select 2 for Latest Rates Currency data and save data into Database");
            string key = System.Console.ReadLine();
            string from = "";
            string to = "";
            var amount = "";
            string date = "";

            if (int.Parse(key) == 1)
            {
                System.Console.WriteLine("Enter from value is required field. format NOK");
                from = System.Console.ReadLine();

                System.Console.WriteLine("Enter to value is required field. format GBP ");
                to = System.Console.ReadLine();

                System.Console.WriteLine("Enter amount is required field.");
                amount = System.Console.ReadLine();

                System.Console.WriteLine("Enter date is optional field format 2022-08-21");
                date = System.Console.ReadLine();
            }

            System.Console.WriteLine("Start Time: " + DateTime.Now);


            switch (int.Parse(key))
            {
                case 1:
                    rateManager.GetConvertCurrencyRates(from, to ,Convert.ToInt32(amount), date);
                    break;

                case 2:
                    //rateManager.GetLatestCurrencyRates();
                    recurringJobManager.AddOrUpdate("run daily",
                         () => rateManager.GetLatestCurrencyRates(),
                         Cron.Daily);

                    break;

                default:
                    break;
            }

            System.Console.WriteLine("End Time: " + DateTime.Now);

            System.Console.WriteLine("Press any key to end the program");

            System.Console.ReadLine();

        }
    }
}
