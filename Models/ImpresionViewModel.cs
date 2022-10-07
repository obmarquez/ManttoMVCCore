using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class ImpresionViewModel
    {
        public int id { get; set; }
        public string valor { get; set; } //area, marca, equipo, inventario, resguardante, testigo
        public string clave { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public string serie { get; set; }
        public bool activo { get; set; }
        public string resguardante { get; set; }
        public string area { get; set; }
        public string extension { get; set; }
        public string inventario { get; set; }
        public string accesorios { get; set; }
        public string falla { get; set; }
        public string diagnostico { get; set; }
        public string dictamen { get; set; }
        public string recibe { get; set; }
        public string realizo { get; set; }
        public string jefe { get; set; }
        public string fecha { get; set; }
    }
}
