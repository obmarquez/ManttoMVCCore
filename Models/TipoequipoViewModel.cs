using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class TipoequipoViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Se requiere esta información")]
        public string equipo { get; set; }
        public int opcion { get; set; }
    }
}
