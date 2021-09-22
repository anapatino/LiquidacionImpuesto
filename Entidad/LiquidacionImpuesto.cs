using System;

namespace Entidad
{
    public class LiquidacionImpuesto
    {
        private long identificacion;
        private double valorIngresoAnuales, valorGastosAnuales, UVT = 30.000,ganancia,tarifa,valorUVT,valorLiquidacion;
        private int tiempoFuncionamiento;
        private string tipoResponsabilidad,nombreEstableciemnto;

        public LiquidacionImpuesto()
        {

        }

        public LiquidacionImpuesto(long identificacion,string nombreEstablecimiento,double valorIngresoAnuales,double valorGastosAnuales ,string tipoResponsabilidad,int tiempoFuncionamiento)
        {
            Identificacion=identificacion;
            NombreEstablecimiento=nombreEstablecimiento;
            ValorIngresoAnual=valorIngresoAnuales;
            ValorGastoAnual=valorGastosAnuales;
            TipoResponsabilidad=tipoResponsabilidad;
            TiempoFuncionamiento = tiempoFuncionamiento;
            Ganancia = ganancia;
            Tarifa = tarifa;
            ValorUVT = valorUVT;
            ValorLiquidacion = valorLiquidacion;
        }
        public long Identificacion { get; set; }
        public string NombreEstablecimiento { get; set; }
        public double ValorIngresoAnual { get; set; }
        public double ValorGastoAnual { get; set; }
        public string TipoResponsabilidad { get; set; }
        public int TiempoFuncionamiento { get; set; }
        public double Ganancia { get; set; }
        public double Tarifa { get; set; }
        public double ValorUVT { get; set; }
        public double ValorLiquidacion { get; set; }

        public void CalcularLiquidacion()
        {
            CalcularGanancia();
            CalcularTarifa();
            CalcularValorUVT();
            ValorLiquidacion = Ganancia * (Tarifa / 100);
        }

        public void CalcularGanancia()
        {
            Ganancia = ValorIngresoAnual - ValorGastoAnual;
        }
        public void CalcularValorUVT()
        {
            ValorUVT=Ganancia / UVT;
        }

        public void CalcularTarifa()
        {
            if (TipoResponsabilidad.ToUpper().Equals("IVA"))
            {
                CalcularResponsableTarifa();
            }
            else 
            {
                CalcularNoResponsableTarifa();
            }
        }

        public void CalcularResponsableTarifa()
        {
            if (Ganancia < 0)
            {
                Tarifa = 0;
            }
            else if (Ganancia < (UVT*100))
            {
                Tarifa = 5;
            }
            else if ((Ganancia >= (UVT * 100))&& (Ganancia <= (UVT * 200)))
            {
                Tarifa = 10;
            }
            else if (Ganancia <= (UVT * 500))
            {
                Tarifa = 15;
            }

        }
        public void CalcularNoResponsableTarifa()
        {
            if (Ganancia > (UVT * 100))
            {
                if (TiempoFuncionamiento < 5)
                {
                    Tarifa = 1;
                }
                else if ((TiempoFuncionamiento >= 5)&& (TiempoFuncionamiento <10))
                {
                    Tarifa = 2;
                }
                else if (TiempoFuncionamiento >= 10)
                {
                    Tarifa = 3;
                }
            }
            else
            {
                Tarifa = 0;
            }
           

        }
    }
}
