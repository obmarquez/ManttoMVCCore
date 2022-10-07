using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class SoporteModel
    {
        public int id { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public int idArea { get; set; }
        public string genero { get; set; }
        public int idServicio { get; set; }

        [Required(ErrorMessage = "Debe indicar la solicitud")]
        [MaxLength(200, ErrorMessage = "Puede escribir hasta 200 caracteres")]
        public string problema { get; set; }
        public DateTime fechaAtencion { get; set; }

        public int idAtendio { get; set; }

        public int idSolicito { get; set; }
        public int nivel { get; set; }
        public string extension { get; set; }

        [Required(ErrorMessage = "Debe indicar la acción que se realizadó")]
        [MaxLength(200, ErrorMessage = "Puede escribir hasta 200 caracteres")]
        public string diagnostico { get; set; }
        public int opcion { get; set; }
    }
}
