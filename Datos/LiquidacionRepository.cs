using System;
using System.IO;
using Entidad;

namespace Datos
{
    public class LiquidacionRepository
    {
        public string ruta = @"ListaPersonaLiquidadasImpuesto.txt";

        public void Guardar(LiquidacionImpuesto paciente)
        {
            using StreamWriter escritor = new(ruta, true);
            escritor.WriteLine($"{paciente.Identificacion};{paciente.NombreEstablecimiento};{paciente.ValorIngresoAnual};{paciente.ValorGastoAnual};{paciente.TipoResponsabilidad};{paciente.TiempoFuncionamiento};{paciente.Ganancia};{paciente.Tarifa};{paciente.ValorUVT };{paciente.ValorLiquidacion}");
         
        }
    }
}
