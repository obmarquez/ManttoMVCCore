using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    [Table("Usuarios")]
    public partial class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Escriba el nombre del usuario.")]
        [MinLength(7, ErrorMessage = "Escriba al menos 7 caracteres.")]
        [MaxLength(7, ErrorMessage = "Escriba máximo 50 caracteres.")]
        public string Nombre { get; set; }
        public string Sal { get; set; }

        [Required(ErrorMessage = "Escriba la clave del usuario.")]
        [MinLength(7, ErrorMessage = "Escriba al menos 7 caracteres.")]
        [MaxLength(50, ErrorMessage = "Escriba máximo 50 caracteres.")]
        public string Clave { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual List<UsuarioRol> Roles { get; set; }
    }
}
