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
                if(dato[4].Equals("CON IVA"))
                {
                    LiquidacionImpuesto responsable = new ResponsableIVA()
                    {
                        Identificacion = long.Parse(dato[0]),
                        NombreEstablecimiento = dato[1],
                        ValorIngresoAnual = decimal.Parse(dato[2]),
                        ValorGastoAnual = decimal.Parse(dato[3]),
                        TipoResponsabilidad = dato[4],
                        TiempoFuncionamiento = int.Parse(dato[5]),
                        Ganancia = decimal.Parse(dato[6]),
                        Tarifa = decimal.Parse(dato[7]),
                        ValorUVT = double.Parse(dato[8]),
                        ValorLiquidacion = decimal.Parse(dato[9])
                    };
                    personas.Add(responsable);
                }
                else if (dato[4].Equals("CON IVA"))
                {
                    LiquidacionImpuesto noResponsable = new NoResponsableIVA()
                    {
                        Identificacion = long.Parse(dato[0]),
                        NombreEstablecimiento = dato[1],
                        ValorIngresoAnual = decimal.Parse(dato[2]),
                        ValorGastoAnual = decimal.Parse(dato[3]),
                        TipoResponsabilidad = dato[4],
                        TiempoFuncionamiento = int.Parse(dato[5]),
                        Ganancia = decimal.Parse(dato[6]),
                        Tarifa = decimal.Parse(dato[7]),
                        ValorUVT = double.Parse(dato[8]),
                        ValorLiquidacion = decimal.Parse(dato[9])
                    };
                    personas.Add(noResponsable);
                }
                else if (dato[4].Equals("RST"))
                {
                    LiquidacionImpuesto regimen = new RegimenSimpleTributacion()
                    {
                        Identificacion = long.Parse(dato[0]),
                        NombreEstablecimiento = dato[1],
                        ValorIngresoAnual = decimal.Parse(dato[2]),
                        ValorGastoAnual = decimal.Parse(dato[3]),
                        TipoResponsabilidad = dato[4],
                        TiempoFuncionamiento = int.Parse(dato[5]),
                        Ganancia = decimal.Parse(dato[6]),
                        Tarifa = decimal.Parse(dato[7]),
                        ValorUVT = double.Parse(dato[8]),
                        ValorLiquidacion = decimal.Parse(dato[9])
                    };
                    personas.Add(regimen);
                }

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
            bool resultado = File.Exists(ruta);
            if (resultado == true)
            {
                List<LiquidacionImpuesto> persona = Consultar();
                foreach (var item in persona)
                {
                    if (item.Identificacion.Equals(identificacion))
                    {
                        return item;
                    }
                }
            }
            
            return null;
        }


    }
}
