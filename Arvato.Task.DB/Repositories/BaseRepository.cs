using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Base.Dto;
using Microsoft.Extensions.Configuration;
using PetaPoco;
using PetaPoco.Providers;
using System;

namespace Arvato.Task.DB.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private string _connectionString;
        private readonly IDatabase _db;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:umbracoDbDSN"];

            _db = DatabaseConfiguration.Build()
                    .UsingConnectionString(_connectionString)
                    .UsingProvider<SqlServerDatabaseProvider>()
                    .Create();
        }
        public int Create(BaseDto model)
        {
            var id = _db.Insert(model);
            return Convert.ToInt32(id);
        }
    }
}
