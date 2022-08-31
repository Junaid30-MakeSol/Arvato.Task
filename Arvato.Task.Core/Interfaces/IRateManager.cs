using Arvato.Task.Fixer.Models.Fixer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.Task.Core.Interfaces
{
    public interface IRateManager
    {
        void GetConvertCurrencyRates(string from, string to, int amount, string date);
        void GetLatestCurrencyRates(string symbols, string bas);
    }
}

