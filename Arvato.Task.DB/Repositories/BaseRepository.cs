using Arvato.Task.DB.Interfaces;
using Arvato.Task.DB.Models.Base.Dto;
using PetaPoco;
using System;

namespace Arvato.Task.DB.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IDatabase _db;
        public BaseRepository(IDatabase db)
        {
            _db = db;
        }
        public int Create(BaseDto model)
        {
            var id = _db.Insert(model);
            return Convert.ToInt32(id);
        }
    }
}
