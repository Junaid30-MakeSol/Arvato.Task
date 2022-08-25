using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Currency.Dto;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using PetaPoco.Providers;
using System;

namespace Arvato.Task.DB.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private string _connectionString;
        private readonly IDatabase _db;
        public CurrencyRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:umbracoDbDSN"];

            _db = DatabaseConfiguration.Build()
                    .UsingConnectionString(_connectionString)
                    .UsingProvider<SqlServerDatabaseProvider>()
                    .Create();
        }
        public int Create(CurrencyDto model)
        {
            var id = _db.Insert(model);
            return Convert.ToInt32(id);
        }
    }
}
