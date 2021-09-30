using System;

namespace Entidad
{
    public abstract class LiquidacionImpuesto
    {
        private long identificacion;
        private double valorUVT,utv=30.000;
        private int tiempoFuncionamiento;
        private string tipoResponsabilidad,nombreEstableciemnto;
        private decimal valorLiquidacion,ganancia,valorIngresoAnuales, valorGastosAnuales,tarifa=0;

        public LiquidacionImpuesto()
        {

        }

        public LiquidacionImpuesto(long identificacion,string nombreEstablecimiento,decimal valorIngresoAnuales,decimal valorGastosAnuales ,string tipoResponsabilidad,int tiempoFuncionamiento)
        {
            Identificacion=identificacion;
            NombreEstablecimiento=nombreEstablecimiento;
            ValorIngresoAnual=valorIngresoAnuales;
            ValorGastoAnual=valorGastosAnuales;
            TipoResponsabilidad=tipoResponsabilidad;
            TiempoFuncionamiento = tiempoFuncionamiento;
        }
        public long Identificacion { get; set; }
        public string NombreEstablecimiento { get; set; }
        public decimal ValorIngresoAnual { get; set; }
        public decimal ValorGastoAnual { get; set; }
        public string TipoResponsabilidad { get; set; }
        public int TiempoFuncionamiento { get; set; }
        public decimal Ganancia { get; set; }
        public decimal Tarifa { get; set; }
        public double ValorUVT { get; set; }
        public decimal ValorLiquidacion { get; set; }

        public void CalcularLiquidacion()
        {
           Ganancia=CalcularGanancia();
           Tarifa=CalcularTarifa();
            CalcularValorUVT();
           ValorLiquidacion = Ganancia * (Tarifa/100);
        }

        public decimal CalcularGanancia()
        {
          decimal ganancia = ValorIngresoAnual - ValorGastoAnual;
            return ganancia;
        }
        public void CalcularValorUVT()
        {
           double valor = (double)Ganancia / utv;
           ValorUVT =  Math.Round(valor, 2);
        }

        public abstract decimal CalcularTarifa();
    
    }
}
