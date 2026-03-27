using Entities.Notificacoes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entidades
{
    [Table("OpcaoResposta")]
    public class OpcaoResposta : Notifica
    {
        public int Id { get; set; }

        [ForeignKey("Resposta")]
        [Column(Order = 1)]
        public int IdResposta { get; set; }

        [ForeignKey("Opcao")]
        [Column(Order = 2)]
        public int IdOpcao { get; set; }
    }
}
