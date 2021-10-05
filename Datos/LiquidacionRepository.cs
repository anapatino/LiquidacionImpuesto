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
            LiquidacionImpuesto persona;
            StreamReader lector = new(ruta);
            string linea;
            while ((linea = lector.ReadLine()) != null)
            {
                persona= MapearLiquidacion(linea);
                personas.Add(persona);

            }
           lector.Close();
           return personas;
        }

        private static LiquidacionImpuesto MapearLiquidacion(string linea)
        {
            string[] dato = linea.Split(';');
            var liquidacion=CrearLiquidacion(dato[4]);
            liquidacion.Identificacion = long.Parse(dato[0]);
            liquidacion.NombreEstablecimiento = dato[1];
            liquidacion.ValorIngresoAnual = decimal.Parse(dato[2]);
            liquidacion.ValorGastoAnual = decimal.Parse(dato[3]);
            liquidacion.TipoResponsabilidad = dato[4];
            liquidacion.TiempoFuncionamiento = int.Parse(dato[5]);
            liquidacion.Ganancia = decimal.Parse(dato[6]);
            liquidacion.Tarifa = double.Parse(dato[7]);
            liquidacion.ValorUVT = double.Parse(dato[8]);
            liquidacion.ValorLiquidacion = decimal.Parse(dato[9]);
            return liquidacion;
        }
        public static LiquidacionImpuesto CrearLiquidacion(string tipoResponsabilidad)
        {
            LiquidacionImpuesto liquidacion;
            if (tipoResponsabilidad.Equals("CON IVA"))
            {
                liquidacion = new ResponsableIVA();
            }
            else if (tipoResponsabilidad.Equals("SIN IVA"))
            {
                liquidacion = new NoResponsableIVA();
            }
            else
            {
                liquidacion = new RegimenSimpleTributacion();
            }
            return liquidacion;
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
