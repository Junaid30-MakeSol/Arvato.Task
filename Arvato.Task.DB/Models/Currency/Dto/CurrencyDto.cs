namespace Arvato.Task.DB.Models.Currency.Dto
{
    [PetaPoco.TableName("Currencies")]
    [PetaPoco.PrimaryKey("CurrencyId")]
    public class CurrencyDto
    {
        [PetaPoco.Column("CurrencyId")]
        public int Id { get; set; }

        [PetaPoco.Column("CurrencyName")]
        public string Name { get; set; }
    }
}
