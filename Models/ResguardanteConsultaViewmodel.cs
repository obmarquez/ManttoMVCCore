using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class ResguardanteConsultaViewmodel
    {
        //Inner join resguardante y areas para el Index de Resguardante
        public int id { get; set; }
        public string resguardante { get; set; }
        public string area { get; set; }
        public string activo { get; set; }
    }
}
