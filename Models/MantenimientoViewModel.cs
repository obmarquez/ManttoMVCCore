using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManttoMVCCore.Models
{
    public class MantenimientoViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Debe elegir el Resguardante.")]
        public int idResguardante { get; set; }

        public string extension { get; set; }

        [Required(ErrorMessage ="Debe indicar un equipo.")]
        public int idEquipo { get; set; }
        public string accesorios { get; set; }

        [Required(ErrorMessage ="Debe indicar la falla encontrada.")]
        public string falla { get; set; }

        [Required(ErrorMessage ="Debe indicar el diagnóstico.")]
        public string diagnostico { get; set; }

        [Required(ErrorMessage ="Debe indicar el dictamén técnico.")]
        public string dictamen { get; set; }

        [Required(ErrorMessage ="Debe indicar quien recibio el equipo.")]
        public int idRecibe { get; set; }

        [Required(ErrorMessage ="Indique quien realizo mantenimiento.")]
        public int idSoporte { get; set; }

        public int opcion { get; set; }

    }
}
