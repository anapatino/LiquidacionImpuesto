using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class RegimenSimpleTributacion:LiquidacionImpuesto
    {
        private int utv = 30000;
        public RegimenSimpleTributacion()
        {

        }

        public RegimenSimpleTributacion(long identificacion, string nombreEstablecimiento, decimal valorIngresoAnuales, decimal valorGastosAnuales, string tipoResponsabilidad, int tiempoFuncionamiento)
       : base(identificacion, nombreEstablecimiento, valorIngresoAnuales, valorGastosAnuales, tipoResponsabilidad, tiempoFuncionamiento)
        {

        }

        public override double CalcularTarifa()
        {
          double tarifa;
            if (Ganancia> (utv*50))
            {
                tarifa = 0.5;
            }
            else
            {
                tarifa = 0;
            }
            return tarifa;
        }
    }
}
