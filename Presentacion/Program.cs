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
                        ConsultarRegistros();
                        break;
                    case 3: seguir = 'N';
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
            Console.WriteLine("2.Consultar");
            Console.WriteLine("3.Salir");
            Console.WriteLine();
            Console.Write(  "Seleccione su opcion->");
            do
            {
                opcion=int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 3);

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

        public static void ConsultarRegistros()
        {
            Console.Clear();
            Console.WriteLine("------Consulta de Datos--------");
            Console.WriteLine();
            var respuesta = liquidacionService.Consultar();
            if (respuesta.Error)
            {
                Console.WriteLine($"     { respuesta.Mensaje}");
            }
            else
            {
                foreach (var item in respuesta.Persona)
                {

                    Console.WriteLine($"Identificacion del Establecimiento ({item.Identificacion})");
                    Console.WriteLine($"Nombre del Establecimiento         ({item.NombreEstablecimiento})");
                    Console.WriteLine($"Valor Ingreso Anual                 ({item.ValorIngresoAnual})");
                    Console.WriteLine($"Valor Gasto   Anual                ({item.ValorGastoAnual})");
                    Console.WriteLine($"Tiempo de Funcionamiento           ({item.TiempoFuncionamiento})");
                    Console.WriteLine($"Tipo de Responsabilidad            ({item.TipoResponsabilidad})");
                    Console.WriteLine("         ");
                    Console.WriteLine($"Ganancia-> ({item.Ganancia})");
                    Console.WriteLine($"Valor UTV-> ({item.ValorUVT})");
                    Console.WriteLine($"Tarifa Aplicada-> ({item.Tarifa})");
                    Console.WriteLine($"Valor Liquidado-> ({item.ValorLiquidacion})");
                   
                    Console.WriteLine("------------------------------------------------");
                }
                Console.Write("Pulse una tecla para salir "); Console.ReadKey();
            }

        }

    }
}
