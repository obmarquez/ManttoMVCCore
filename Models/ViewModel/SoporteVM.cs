using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models.ViewModel
{
    public class SoporteVM
    {
        public int id { get; set; }
        public string fechaSolicitud { get; set; }
        public string resguardante { get; set; }       
        public string problema { get; set; }
        public string diagnostico { get; set; }
        public string servicio { get; set; }
        public string area { get; set; }
        public string tipoServicio { get; set; }
    }
}
