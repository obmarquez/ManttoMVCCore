using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class ResguardanteViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Debe indicar un resguardante")]
        public string resguardante { get; set; }
        public int area { get; set; }
        public bool activo { get; set; }
        public int opcion { get; set; }
    }
}
