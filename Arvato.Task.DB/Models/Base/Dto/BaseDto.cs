using System;

namespace Arvato.Task.DB.Models.Base.Dto
{
    [PetaPoco.TableName("Base")]
    [PetaPoco.PrimaryKey("BaseId")]
    public class BaseDto
    {
        [PetaPoco.Column("BaseId")]
        public int Id { get; set; }

        [PetaPoco.Column("BaseName")]
        public string Name { get; set; }
        [PetaPoco.Column("BaseDate")]
        public DateTime Date { get; set; }
    }
}
