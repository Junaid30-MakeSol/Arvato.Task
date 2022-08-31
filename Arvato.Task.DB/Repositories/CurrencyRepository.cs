using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Currency.Dto;
using PetaPoco;
using System;

namespace Arvato.Task.DB.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IDatabase _db;

        public CurrencyRepository(IDatabase db)
        {
            _db = db;
        }
        public int Create(CurrencyDto model)
        {
            var id = _db.Insert(model);
            return Convert.ToInt32(id);
        }

        public void Delete()
        {
            var sql = "Truncate table [Arvato.Task].[dbo].[Currencies]";
            _db.Execute(sql);
        }
    }
}
