using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entidades
{
    public class AplicationUser : IdentityUser
    {
        [Column("USER_CPF")]
        public string CPF { get; set; }
    }
}
