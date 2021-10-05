using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class NoResponsableIVA:LiquidacionImpuesto
    {
        private const int utv = 30000;
        public NoResponsableIVA()
        {

        }
        public NoResponsableIVA(long identificacion, string nombreEstablecimiento, decimal valorIngresoAnuales, decimal valorGastosAnuales, string tipoResponsabilidad, int tiempoFuncionamiento)
        :base( identificacion, nombreEstablecimiento, valorIngresoAnuales,  valorGastosAnuales, tipoResponsabilidad, tiempoFuncionamiento)
        {

        }

        public override double CalcularTarifa()
        {
            double tarifa;
            if (Ganancia > (utv * 100))
            {
                if (TiempoFuncionamiento <= 5)
                {
                    tarifa = 0.1;
                }
                else if ((TiempoFuncionamiento > 5) && (TiempoFuncionamiento < 10))
                {
                    tarifa = 0.2;
                }
                else 
                {
                    tarifa = 0.3;
                }
            }
            else
            {
                tarifa = 0;
            }
           
            return tarifa;
        }
    }
}
