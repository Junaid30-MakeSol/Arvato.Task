using Arvato.Task.Console;
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
            IRecurringJobManager recurringJobManager = new RecurringJobManager();

            startup.Configure(appBuilder);

            //configure console logging
            serviceProvider.GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogDebug("Logger is working!");


            // Get Service and call method

            System.Console.WriteLine("Select what you want to do");
            System.Console.WriteLine("Select 1 for Converting Currency rate");
            System.Console.WriteLine("Select 2 for Latest Currency data");
            string key = System.Console.ReadLine();
            string from = "";
            string to = "";
            var amount = "";
            string date = "";
            string bas = "";
            string symbols = "";
            if (int.Parse(key) == 1)
            {
                
                System.Console.WriteLine("Enter To Value format GBP ");
                to = System.Console.ReadLine();

                System.Console.WriteLine("Enter From Value format NOK");
                from = System.Console.ReadLine();

                System.Console.WriteLine("Enter amount");
                amount = System.Console.ReadLine();

                System.Console.WriteLine("Enter date is optional format 2022-08-21");
                date = System.Console.ReadLine();
            }

            if (int.Parse(key) == 2)
            {
                System.Console.WriteLine("Enter symbols format GBP,JPY but its optional ");
                symbols = System.Console.ReadLine();

                System.Console.WriteLine("Enter base format USD but its optional");
                bas = System.Console.ReadLine();
            }

        


            System.Console.WriteLine("Start Time: " + DateTime.Now);


            switch (int.Parse(key))
            {
                case 1:
                    var currencyConversion = serviceProvider.GetService<IFixerManager>();
                    currencyConversion.GetConvert(to,from,Convert.ToInt32(amount),date);
                    break;

                case 2:
                    var currencyDate = serviceProvider.GetService<IFixerManager>();
                   //currencyDate.GetLatestCurrency(symbols,bas);
                    recurringJobManager.AddOrUpdate("Run daily",
                         () => currencyDate.GetLatestCurrency(symbols, bas),
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
