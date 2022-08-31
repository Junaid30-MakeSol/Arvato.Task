using System;

namespace Arvato.Task.DB.Models.Rate.Dto
{
    [PetaPoco.TableName("Rates")]
    [PetaPoco.PrimaryKey("RateId")]
    public class RateDto
    {
        [PetaPoco.Column("RateId")]
        public int Id { get; set; }

        [PetaPoco.Column("RateValue")]
        public double Value { get; set; }
        [PetaPoco.Column("RateCurrencyId")]
        public int CurrencyId { get; set; }
        [PetaPoco.Column("RateBaseCurrencyId")]
        public int BaseCurrencyId { get; set; }
        [PetaPoco.Column("RateDate")]
        public DateTime BaseDate { get; set; }
    }
}
