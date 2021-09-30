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
                    case 1: Guardar();
                        break;
                    case 2:
                        ConsultarRegistros();
                        break;
                    case 3: Eliminar();
                        break;
                    case 4:
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
            Console.WriteLine("2.Consultar");
            Console.WriteLine("3.Eliminar");
            Console.WriteLine("4.Salir");
            Console.WriteLine();
            Console.Write("Seleccione su opcion->");
            do
            {
                opcion = int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 3);

            return opcion;
        }
        private static void Guardar()
        {
            Console.Clear();
            Console.WriteLine("---Registro de Usuario---");
            var persona = RegistrarDatos();
            string mensaje = liquidacionService.Guarda(persona);
            Console.WriteLine($"     { mensaje}");
            Console.Write("          Pulse una tecla para salir "); Console.ReadKey();
        }

        public static LiquidacionImpuesto RegistrarDatos()
        {
            long identificacion;
            string nombreEstablecimiento, tipoResponsabilidad;
            decimal valorIngresoAnual, valorGastosAnual;
            int tiempoFuncionamiento;
            Console.Clear();
            identificacion = ValidarIdentificacion();
            Console.Write("Nombre del Establecimiento         :"); nombreEstablecimiento = Console.ReadLine();
            Console.Write("Valor Ingreso Anual                :"); valorIngresoAnual = decimal.Parse(Console.ReadLine());
            Console.Write("Valor Gasto   Anual                :"); valorGastosAnual = decimal.Parse(Console.ReadLine());
            Console.Write("Tiempo de Funcionamiento           :"); tiempoFuncionamiento = int.Parse(Console.ReadLine());
            tipoResponsabilidad = ValidarResponsabilidad();

            if (tipoResponsabilidad.Equals("CON IVA"))
            {

                LiquidacionImpuesto responsable = new ResponsableIVA(identificacion, nombreEstablecimiento, valorIngresoAnual, valorGastosAnual, tipoResponsabilidad, tiempoFuncionamiento);
                responsable.CalcularLiquidacion();
                Console.WriteLine(responsable.Tarifa);
                Console.WriteLine(responsable.Ganancia);
                Console.WriteLine(responsable.ValorLiquidacion);
                return responsable;
            }
            else if (tipoResponsabilidad.Equals("SIN IVA"))
            {
                LiquidacionImpuesto noResponsable = new ResponsableIVA(identificacion, nombreEstablecimiento, valorIngresoAnual, valorGastosAnual, tipoResponsabilidad, tiempoFuncionamiento);
                noResponsable.CalcularLiquidacion();
                Console.WriteLine(noResponsable.Tarifa);
                Console.WriteLine(noResponsable.Ganancia);
                Console.WriteLine(noResponsable.ValorLiquidacion);
                return noResponsable;
            }
            else 
            {
                LiquidacionImpuesto regimen = new RegimenSimpleTributacion(identificacion, nombreEstablecimiento, valorIngresoAnual, valorGastosAnual, tipoResponsabilidad, tiempoFuncionamiento);
                regimen.CalcularLiquidacion();
                Console.WriteLine(regimen.Tarifa);
                Console.WriteLine(regimen.Ganancia);
                Console.WriteLine(regimen.ValorLiquidacion);
                return regimen;
            }
        }
        public static long ValidarIdentificacion()
        {
            long identificacion;
            string respuesta;
            do
            {
                Console.Write("Identificacion del Establecimiento :"); identificacion = long.Parse(Console.ReadLine());
                respuesta=liquidacionService.Busca(identificacion);
                Console.WriteLine(respuesta);
            } while (!respuesta.Equals("Identificacion Generada correctamente"));
          
            return identificacion;
        }
        public static string ValidarResponsabilidad()
        {
            int opcion;
            string tipoResponsabilidad;
            do
            {
                Console.Write("Tipo de Responsabilidad    ( 1.RESPONSABLE IVA    2.NO RESPONSABLE IVA 3.REGIMEN TRIBUTARIO  ) :");
                opcion = int.Parse(Console.ReadLine());
            } while (opcion < 1 && opcion > 3);

            if (opcion == 1)
            {
                tipoResponsabilidad = "CON IVA";
            }
            else if(opcion == 2)
            {
                tipoResponsabilidad = "SIN IVA";
            }
            else
            {
                tipoResponsabilidad = "RST";
            }

            return tipoResponsabilidad;
        }
        public static void ConsultarRegistros()
        {
            Console.Clear();
            Console.WriteLine("---------------------------");
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
                   
                    Console.WriteLine("_____________________________________");
                }
                Console.Write("Pulse una tecla para salir "); Console.ReadKey();
            }

        }
 
        public static void Eliminar()
        {
            Console.Clear();
            Console.WriteLine("------Eliminar por Identificacion --------");
            Console.WriteLine();
            Console.Write(" Nro Liquidacion :"); long numeroIdentificacion = long.Parse(Console.ReadLine());
            string mensajeEliminacion = liquidacionService.Eliminar(numeroIdentificacion);
            Console.WriteLine($"    { mensajeEliminacion} ");
            Console.WriteLine();
            Console.Write("   Pulse una tecla para salir "); Console.ReadKey();
        }

    }
}
