using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entidades
{
    [Table("Empresa")]
    public class Empresa : Base
    {
        public string Documento { get; set; }

        public bool Ativo { get; set; }
    }
}
