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
        [PetaPoco.Column("RateBaseId")]
        public int BaseId { get; set; }
    }
}
