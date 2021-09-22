using System;
using Logica;
using Entidad;

namespace Presentacion
{
    class Program
    {
        private static readonly LiquidacionService liquidacionService = new LiquidacionService();
        static void Main(string[] args)
        {
            char seguir = 'S';
            do
            {
                int opcion = Menu();
                switch (opcion)
                {
                    case 1:Guardar();
          
                        break;
                    case 2:
                        seguir = 'N';
                        break;

                }
            } while (seguir == 'S');
           

        }
        
        public static int Menu()
        {
            int opcion;
            Console.Clear();
            Console.WriteLine("-----Liquidaciones de Impuestos---");
            Console.WriteLine("1.Registrar Establecimiento");
            Console.WriteLine("2.Salir");
            Console.WriteLine();
            Console.Write(  "Seleccione su opcion->");
            do
            {
                opcion=int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 2);

            return opcion;
        }
        private static void Guardar()
        {
            Console.Clear();
            Console.WriteLine("---Registro de Usuario---");
            var persona = RegistrarDatos();
            string mensaje= liquidacionService.Guarda(persona);
            Console.WriteLine($"     { mensaje}");
            Console.Write("          Pulse una tecla para salir "); Console.ReadKey();
        }

        public static LiquidacionImpuesto RegistrarDatos()
        {
            long identificacion;
            string nombreEstablecimiento,tipoResponsabilidad;
            double valorIngresoAnual, valorGastosAnual;
            int  tiempoFuncionamiento;
            LiquidacionImpuesto persona;
            Console.Clear();
            Console.Write("Identificacion del Establecimiento :"); identificacion = long.Parse(Console.ReadLine());
            Console.Write("Nombre del Establecimiento         :"); nombreEstablecimiento = Console.ReadLine();
            Console.Write("Valor Ingreso Anual                :"); valorIngresoAnual = double.Parse(Console.ReadLine());
            Console.Write("Valor Gasto   Anual                :"); valorGastosAnual = double.Parse(Console.ReadLine());
            Console.Write("Tiempo de Funcionamiento           :"); tiempoFuncionamiento =int.Parse(Console.ReadLine());
            Console.Write("Tipo de Responsabilidad            :"); tipoResponsabilidad = Console.ReadLine();

            persona = new(identificacion, nombreEstablecimiento,  valorIngresoAnual, valorGastosAnual, tipoResponsabilidad,tiempoFuncionamiento);
            persona.CalcularLiquidacion();
            return persona;

        }

        public static void Consultar(LiquidacionImpuesto persona)
        {
            Console.Clear();
            Console.WriteLine($"Identificacion del Establecimiento ({persona.Identificacion})");
            Console.WriteLine($"Nombre del Establecimiento         ({persona.NombreEstablecimiento})"); 
            Console.WriteLine($"Valor Ingreso Anual                 ({persona.ValorIngresoAnual})");
            Console.WriteLine($"Valor Gasto   Anual                ({persona.ValorGastoAnual})"); 
            Console.WriteLine($"Tiempo de Funcionamiento           ({persona.TiempoFuncionamiento})"); 
            Console.WriteLine($"Tipo de Responsabilidad            ({persona.TipoResponsabilidad})");
            Console.WriteLine("             -------------------------------");
            Console.WriteLine($"Ganancia-> ({persona.Ganancia})");
            Console.WriteLine($"Valor UTV-> ({persona.ValorUVT})");
            Console.WriteLine($"Tarifa Aplicada-> ({persona.Tarifa})");
            Console.WriteLine($"Valor Liquidado-> ({persona.ValorLiquidacion})");
            Console.ReadKey();
        }
    }
}
