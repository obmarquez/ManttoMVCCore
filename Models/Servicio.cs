using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class Servicio
    {
        public int idServicio { get; set; }

        [Required(ErrorMessage = "Debe indicar un Servicio")]
        [MaxLength(50, ErrorMessage = "Puede escribir hasta 50 caracteres")]
        public string cServicio { get; set; }
        public int opcion { get; set; }
    }
}
