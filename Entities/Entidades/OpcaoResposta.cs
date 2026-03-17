using Entities.Notificacoes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entidades
{
    [Table("OpcaoResposta")]
    public class OpcaoResposta : Notifica
    {
        public uint Id { get; set; }

        [ForeignKey("Resposta")]
        [Column(Order = 1)]
        public uint IdResposta { get; set; }

        [ForeignKey("Opcao")]
        [Column(Order = 1)]
        public uint IdOpcao { get; set; }
    }
}
