using Entidad;
using System.Collections.Generic;
using System.IO;

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

        public List<LiquidacionImpuesto> Consultar()
        {
            List<LiquidacionImpuesto> personas = new();
            StreamReader lector = new(ruta);
            string linea;
            while ((linea = lector.ReadLine()) != null)
            {
                string[] dato = linea.Split(';');

                LiquidacionImpuesto persona = new LiquidacionImpuesto()
                    {
                        Identificacion = long.Parse(dato[0]),
                        NombreEstablecimiento = dato[1],
                        ValorIngresoAnual =double.Parse(dato[2]),
                        ValorGastoAnual = double.Parse(dato[3]),
                        TipoResponsabilidad = dato[4],
                        TiempoFuncionamiento = int.Parse(dato[5]),
                        Ganancia = double.Parse(dato[6]),
                        Tarifa = double.Parse(dato[7]),
                        ValorUVT = double.Parse(dato[8]),
                        ValorLiquidacion = double.Parse(dato[9])
                    };
                personas.Add(persona);

            }
           lector.Close();
           return personas;
        }

        public void Eliminar(long identificacion)
        {
            List<LiquidacionImpuesto> persona = Consultar();
            File.Delete(ruta);
            foreach (var item in persona)
            {
                if (!item.Identificacion.Equals(identificacion))
                {
                    Guardar(item);
                }
            }

        }
        public LiquidacionImpuesto Buscar(long identificacion)
        {
            List<LiquidacionImpuesto> persona = Consultar();
            foreach (var item in persona)
            {
                if (item.Identificacion.Equals(identificacion))
                {
                    return item;
                }
            }
            return null;
        }


    }
}
