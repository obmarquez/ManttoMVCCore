using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class Consultas
    {
        public int id { get; set; }
        public string resguardante { get; set; }
        public string recibe { get; set; }
        public string realiza { get; set; }
        public string area { get; set; }
        public int total { get; set; }
    }
}
