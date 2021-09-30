using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ResponsableIVA : LiquidacionImpuesto
    {
        private int utv = 30000;
        public ResponsableIVA()
        {

        }
        public ResponsableIVA(long identificacion, string nombreEstablecimiento, decimal valorIngresoAnuales, decimal valorGastosAnuales, string tipoResponsabilidad, int tiempoFuncionamiento)
         :base( identificacion,nombreEstablecimiento,valorIngresoAnuales, valorGastosAnuales, tipoResponsabilidad,tiempoFuncionamiento)
        {

        }

        public override decimal CalcularTarifa()
        {
           decimal tarifa;
            if (Ganancia < 0)
            {
                tarifa = 0;
            }
            else if (Ganancia < (utv * 100))
            {
                tarifa = 5;
            }
            else if ((Ganancia >= (utv * 100)) && (Ganancia < (utv * 200)))
            {
                tarifa = 10;
            }
            else if (Ganancia >= (utv * 500))
            {
                tarifa = 15;
            }
            else
            {
                tarifa = 0;
            }
            return tarifa;
        }
    }
}
