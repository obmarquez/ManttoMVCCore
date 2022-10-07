using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class EquipoViewModel
    {
        public int id { get; set; }
        public string inventario { get; set; }
        public string clave { get; set; }
        public string descripcion { get; set; }
        public string serie { get; set; }
        //22/03/2021
        public int opcion { get; set; }
    }
}
