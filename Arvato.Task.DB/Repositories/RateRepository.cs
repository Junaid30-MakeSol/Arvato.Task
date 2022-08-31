using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Rate.Dto;
using PetaPoco;

namespace Arvato.Task.DB.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly IDatabase _db;
        public RateRepository(IDatabase db)
        {
            _db = db;
        }
        public void Create(RateDto model)
        {
            _db.Insert(model);
        }

        public void Delete()
        {
            var sql = "Truncate table [Arvato.Task].[dbo].[Rates]";
            _db.Execute(sql);
        }
    }
}
