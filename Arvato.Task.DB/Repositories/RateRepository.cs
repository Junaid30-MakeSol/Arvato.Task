using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Rate.Dto;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using PetaPoco.Providers;

namespace Arvato.Task.DB.Repositories
{
    public class RateRepository : IRateRepository
    {
        private string _connectionString;
        private readonly IDatabase _db;
        public RateRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:umbracoDbDSN"];

            _db = DatabaseConfiguration.Build()
                    .UsingConnectionString(_connectionString)
                    .UsingProvider<SqlServerDatabaseProvider>()
                    .Create();
        }
        public void Create(RateDto model)
        {
            _db.Insert(model);
        }
    }
}
