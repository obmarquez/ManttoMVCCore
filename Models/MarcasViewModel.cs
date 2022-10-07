using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class MarcasViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Debe indicar una marca")]
        [MaxLength(50, ErrorMessage = "Puede escribir hasta 50 caracteres")]
        public string marca { get; set; }
        public int opcion { get; set; }
    }
}
