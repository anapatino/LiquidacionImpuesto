using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidad;

namespace Logica
{
    public class LiquidacionConsultaResponse
    {
        public List<LiquidacionImpuesto> Persona { get; set; }
        public string Mensaje { get; set; }
        public bool Error { get; set; }
        public LiquidacionConsultaResponse(List<LiquidacionImpuesto> persona)
        {
            Persona = persona;
            Error = false;
        }
        public LiquidacionConsultaResponse(string mensaje)
        {
            Mensaje = mensaje;
            Error = true;
        }
    }
}
