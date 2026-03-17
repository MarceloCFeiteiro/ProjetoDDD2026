using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entidades
{
    [Table("Opcao")]
    public class Opcao : Base
    {
        public uint Peso { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("Pergunta")]
        [Column(Order = 1)]
        public uint IdPergunta { get; set; }

        [JsonIgnore]
        public virtual Pergunta? Pergunta { get; set; }
    }
}
