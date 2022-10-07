using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class TestigoViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Debe indicar un nombre")]
        public string testigo { get; set; }

        public bool activo { get; set; }

        public int opcion { get; set; }
    }
}
